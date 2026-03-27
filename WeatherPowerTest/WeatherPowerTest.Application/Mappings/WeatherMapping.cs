using System.Collections.Generic;
using System.Linq;
using WeatherPowerTest.Application.DTOs;
using WeatherPowerTest.Domain.Entities;

namespace WeatherPowerTest.Application.Mappings
{
    /// <summary>
    /// Маппинг DTO
    /// </summary>
    public static class WeatherMapping
    {
        public static WeatherDataDto ToDto(this WeatherData domain, bool isFromCache = false)
        {
            if (domain == null)
                return null;

            return new WeatherDataDto
            {
                Current = domain.Current.ToDto(),
                Hourly = domain.HourlyForecasts.ToDtoList(),
                Forecast = domain.DailyForecasts.ToDtoList(),
                RetrievedAt = domain.RetrievedAt,
                IsFromCache = isFromCache
            };
        }

        /// <summary>
        /// Маппинг CurrentWeather -> CurrentWeatherDto
        /// </summary>
        public static CurrentWeatherDto ToDto(this CurrentWeather domain)
        {
            if (domain == null)
                return null;

            return new CurrentWeatherDto
            {
                TempC = domain.Temperature.Celsius,
                TempF = domain.Temperature.Fahrenheit,
                ConditionText = domain.Condition,
                ConditionIcon = domain.ConditionIconUrl,
                WindKph = domain.Wind.SpeedKph,
                Humidity = domain.Humidity.Percent,
                LastUpdated = domain.LastUpdated.ToString("dd.MM.yyyy HH:mm")
            };
        }

        /// <summary>
        /// Маппинг списка HourlyForecast -> List<HourlyForecastDto>
        /// </summary>
        public static List<HourlyForecastDto> ToDtoList(this IReadOnlyList<HourlyForecast> domainList)
        {
            if (domainList == null || !domainList.Any())
                return new List<HourlyForecastDto>();

            return domainList.Select(h => new HourlyForecastDto
            {
                Time = h.TimeDisplay,
                TempC = h.Temperature.Celsius,
                ConditionText = h.Condition,
                ConditionIcon = h.ConditionIconUrl,
                ChanceOfRain = h.ChanceOfRain
            }).ToList();
        }

        /// <summary>
        /// Маппинг списка DailyForecast -> List<DailyForecastDto>
        /// </summary>
        public static List<DailyForecastDto> ToDtoList(this IReadOnlyList<DailyForecast> domainList)
        {
            if (domainList == null || !domainList.Any())
                return new List<DailyForecastDto>();

            return domainList.Select(d => new DailyForecastDto
            {
                Date = d.DateDisplay,
                MaxTempC = d.MaxTemperature.Celsius,
                MinTempC = d.MinTemperature.Celsius,
                ConditionText = d.Condition,
                ConditionIcon = d.ConditionIconUrl
            }).ToList();
        }
    }
}
