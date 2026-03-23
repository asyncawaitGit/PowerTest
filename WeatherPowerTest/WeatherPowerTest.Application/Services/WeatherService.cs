using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPowerTest.Application.Interfaces;
using WeatherPowerTest.Domain.Entities;
using WeatherPowerTest.Domain.Exceptions;
using WeatherPowerTest.Domain.Interfaces;
using WeatherPowerTest.Domain.ValueObjects;

namespace WeatherPowerTest.Application.Services
{
    /// <summary>
    /// Сервис погоды с бизнес-логикой
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherApiClient _apiClient;
        private readonly IWeatherCache _cache;

        public WeatherService(IWeatherApiClient apiClient, IWeatherCache cache = null)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _cache = cache;
        }

        public async Task<WeatherData> GetWeatherDataAsync()
        {
            // Проверяем кэш
            if (_cache != null && await _cache.IsValidAsync())
            {
                var cachedData = await _cache.GetAsync();
                if (cachedData != null)
                {
                    return cachedData;
                }
            }

            // Получаем данные из API
            var weatherData = await FetchWeatherDataFromApiAsync();

            if (_cache != null)
            {
                await _cache.SetAsync(weatherData);
            }

            return weatherData;
        }

        public async Task<CurrentWeather> GetCurrentWeatherAsync()
        {
            var weatherData = await GetWeatherDataAsync();
            return weatherData.Current;
        }

        public async Task<IReadOnlyList<HourlyForecast>> GetHourlyForecastAsync()
        {
            var weatherData = await GetWeatherDataAsync();
            return weatherData.HourlyForecasts;
        }

        public async Task<IReadOnlyList<DailyForecast>> GetDailyForecastAsync(int days = 3)
        {
            var weatherData = await GetWeatherDataAsync();
            return weatherData.DailyForecasts.Take(days).ToList();
        }

        private async Task<WeatherData> FetchWeatherDataFromApiAsync()
        {
            try
            {
                // Параллельные запросы
                var currentTask = _apiClient.GetCurrentWeatherAsync();
                var forecastTask = _apiClient.GetForecastAsync(3);

                await Task.WhenAll(currentTask, forecastTask);

                var currentResponse = await currentTask;
                var forecastResponse = await forecastTask;

                // Проверка успешности запросов
                if (!currentResponse.IsSuccess)
                {
                    throw new WeatherApiException(
                        $"Ошибка получения текущей погоды: {currentResponse.ErrorMessage}",
                        "current.json",
                        currentResponse.StatusCode);
                }

                if (!forecastResponse.IsSuccess)
                {
                    throw new WeatherApiException(
                        $"Ошибка получения прогноза: {forecastResponse.ErrorMessage}",
                        "forecast.json",
                        forecastResponse.StatusCode);
                }

                var currentJson = JObject.Parse(currentResponse.RawJson);
                var forecastJson = JObject.Parse(forecastResponse.RawJson);

                var locationName = currentJson["location"]?["name"]?.ToString();
                if (string.IsNullOrEmpty(locationName))
                {
                    throw new WeatherValidationException("Не удалось определить локацию", "Location");
                }

                // Парсим данные
                var currentWeather = ParseCurrentWeather(currentJson);
                var hourlyForecasts = ParseHourlyForecast(forecastJson);
                var dailyForecasts = ParseDailyForecast(forecastJson);

                return new WeatherData(currentWeather, hourlyForecasts, dailyForecasts);
            }
            catch (WeatherException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new WeatherException("Ошибка получения данных о погоде", ex);
            }
        }

        private CurrentWeather ParseCurrentWeather(JObject json)
        {
            var current = json["current"];
            if (current == null)
                throw new WeatherValidationException("Не найден элемент 'current' в ответе API", "Current");

            var condition = current["condition"];
            if (condition == null)
                throw new WeatherValidationException("Не найден элемент 'condition' в ответе API", "Condition");

            var temperature = new Temperature((double)current["temp_c"]);
            var wind = new Wind((double)current["wind_kph"]);
            var humidity = new Humidity((int)current["humidity"]);

            return new CurrentWeather(
                temperature: temperature,
                condition: (string)condition["text"],
                conditionIconUrl: "https:" + (string)condition["icon"],
                wind: wind,
                humidity: humidity,
                lastUpdated: DateTime.Parse((string)current["last_updated"])
            );
        }

        private IReadOnlyList<HourlyForecast> ParseHourlyForecast(JObject json)
        {
            var hourlyList = new List<HourlyForecast>();
            var now = DateTime.Now;

            var forecast = json["forecast"];
            if (forecast == null)
                return hourlyList;

            var forecastDays = forecast["forecastday"] as JArray;
            if (forecastDays == null || forecastDays.Count == 0)
                return hourlyList;

            // Оставшиеся часы текущего дня
            var today = forecastDays[0];
            var todayHours = today["hour"] as JArray;

            if (todayHours != null)
            {
                foreach (var hour in todayHours)
                {
                    var hourTime = DateTime.Parse((string)hour["time"]);
                    if (hourTime >= now)
                    {
                        hourlyList.Add(ParseHour(hour));
                    }
                }
            }

            // Все часы следующего дня
            if (forecastDays.Count > 1)
            {
                var nextDay = forecastDays[1];
                var nextHours = nextDay["hour"] as JArray;

                if (nextHours != null)
                {
                    foreach (var hour in nextHours)
                    {
                        hourlyList.Add(ParseHour(hour));
                    }
                }
            }

            return hourlyList;
        }

        private HourlyForecast ParseHour(JToken hour)
        {
            var condition = hour["condition"];
            if (condition == null)
                throw new WeatherValidationException("Не найден элемент 'condition' в часовом прогнозе", "HourCondition");

            var timeStr = (string)hour["time"];
            var time = DateTime.Parse(timeStr);
            var temperature = new Temperature((double)hour["temp_c"]);

            double chanceOfRain = 0;
            if (hour["chance_of_rain"] != null)
            {
                chanceOfRain = (double)hour["chance_of_rain"];
            }

            return new HourlyForecast(
                time: time,
                temperature: temperature,
                condition: (string)condition["text"],
                conditionIconUrl: "https:" + (string)condition["icon"],
                chanceOfRain: chanceOfRain
            );
        }

        private IReadOnlyList<DailyForecast> ParseDailyForecast(JObject json)
        {
            var dailyList = new List<DailyForecast>();

            var forecast = json["forecast"];
            if (forecast == null)
                return dailyList;

            var forecastDays = forecast["forecastday"] as JArray;
            if (forecastDays == null)
                return dailyList;

            foreach (var day in forecastDays)
            {
                var dayData = day["day"];
                if (dayData == null)
                    continue;

                var condition = dayData["condition"];
                if (condition == null)
                    continue;

                var date = DateTime.Parse((string)day["date"]);
                var maxTemp = new Temperature((double)dayData["maxtemp_c"]);
                var minTemp = new Temperature((double)dayData["mintemp_c"]);

                dailyList.Add(new DailyForecast(
                    date: date,
                    maxTemperature: maxTemp,
                    minTemperature: minTemp,
                    condition: (string)condition["text"],
                    conditionIconUrl: "https:" + (string)condition["icon"]
                ));
            }

            return dailyList;
        }
    }
}
