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
            return new WeatherDataDto
            {
                Current = new CurrentWeatherDto
                {
                    TempC = domain.Current.Temperature.Celsius,
                    TempF = domain.Current.Temperature.Fahrenheit,
                    ConditionText = domain.Current.Condition,
                    ConditionIcon = domain.Current.ConditionIconUrl,
                    WindKph = domain.Current.Wind.SpeedKph,
                    Humidity = domain.Current.Humidity.Percent,
                    LastUpdated = domain.Current.LastUpdated.ToString("dd.MM.yyyy HH:mm")
                },

                Hourly = domain.HourlyForecasts.Select(h => new HourlyForecastDto
                {
                    Time = h.TimeDisplay,
                    TempC = h.Temperature.Celsius,
                    ConditionText = h.Condition,
                    ConditionIcon = h.ConditionIconUrl,
                    ChanceOfRain = h.ChanceOfRain
                }).ToList(),

                Forecast = domain.DailyForecasts.Select(d => new DailyForecastDto
                {
                    Date = d.DateDisplay,
                    MaxTempC = d.MaxTemperature.Celsius,
                    MinTempC = d.MinTemperature.Celsius,
                    ConditionText = d.Condition,
                    ConditionIcon = d.ConditionIconUrl
                }).ToList(),

                RetrievedAt = domain.RetrievedAt,

                IsFromCache = isFromCache
            };
        }
    }
}
