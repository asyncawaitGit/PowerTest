using System;
using WeatherPowerTest.Domain.ValueObjects;

namespace WeatherPowerTest.Domain.Entities
{
    /// <summary>
    /// Почасовой прогноз
    /// </summary>
    public class HourlyForecast
    {
        public DateTime Time { get; private set; }
        public Temperature Temperature { get; private set; }
        public string Condition { get; private set; }
        public string ConditionIconUrl { get; private set; }
        public double ChanceOfRain { get; private set; }

        public HourlyForecast(
            DateTime time,
            Temperature temperature,
            string condition,
            string conditionIconUrl,
            double chanceOfRain)
        {
            Time = time;
            Temperature = temperature ?? throw new ArgumentNullException(nameof(temperature));
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
            ConditionIconUrl = conditionIconUrl ?? throw new ArgumentNullException(nameof(conditionIconUrl));

            // Жаль что в framework нет clamp((((
            ChanceOfRain = chanceOfRain < 0 ? 0 : (chanceOfRain > 100 ? 100 : chanceOfRain);
        }

        public string TimeDisplay => Time.ToString("HH:mm");
        public string FormattedTemperature => $"{Temperature.Celsius:F0}°C";
        public string FormattedChanceOfRain => $"{ChanceOfRain:F0}%";
    }
}
