using System;
using WeatherPowerTest.Domain.ValueObjects;

namespace WeatherPowerTest.Domain.Entities
{
    /// <summary>
    /// Текущая погода
    /// </summary>
    public class CurrentWeather
    {
        public Temperature Temperature { get; private set; }
        public string Condition { get; private set; }
        public string ConditionIconUrl { get; private set; }
        public Wind Wind { get; private set; }
        public Humidity Humidity { get; private set; }
        public DateTime LastUpdated { get; private set; }

        public CurrentWeather(
           Temperature temperature,
           string condition,
           string conditionIconUrl,
           Wind wind,
           Humidity humidity,
           DateTime lastUpdated)
        {
            Temperature = temperature ?? throw new ArgumentNullException(nameof(temperature));

            Condition = string.IsNullOrWhiteSpace(condition)
                ? throw new ArgumentException("Condition cannot be empty", nameof(condition))
                : condition;

            ConditionIconUrl = conditionIconUrl ?? throw new ArgumentNullException(nameof(conditionIconUrl));

            Wind = wind ?? throw new ArgumentNullException(nameof(wind));

            Humidity = humidity ?? throw new ArgumentNullException(nameof(humidity));

            LastUpdated = lastUpdated;
        }

        public string FormattedTemperature => $"{Temperature.Celsius:F0}°C";
        public string FormattedWind => $"{Wind.SpeedKph} км/ч";
        public string FormattedHumidity => $"{Humidity.Percent}%";
    }
}
