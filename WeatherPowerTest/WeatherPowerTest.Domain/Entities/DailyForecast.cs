using System;
using WeatherPowerTest.Domain.ValueObjects;

namespace WeatherPowerTest.Domain.Entities
{
    /// <summary>
    /// Дневной прогноз
    /// </summary>
    public class DailyForecast
    {
        public DateTime Date { get; private set; }
        public Temperature MaxTemperature { get; private set; }
        public Temperature MinTemperature { get; private set; }
        public string Condition { get; private set; }
        public string ConditionIconUrl { get; private set; }

        public DailyForecast(
            DateTime date,
            Temperature maxTemperature,
            Temperature minTemperature,
            string condition,
            string conditionIconUrl)
        {
            Date = date.Date;
            MaxTemperature = maxTemperature ?? throw new ArgumentNullException(nameof(maxTemperature));
            MinTemperature = minTemperature ?? throw new ArgumentNullException(nameof(minTemperature));
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
            ConditionIconUrl = conditionIconUrl ?? throw new ArgumentNullException(nameof(conditionIconUrl));

            if (maxTemperature.Celsius < minTemperature.Celsius)
                // Жаль нет udouble, но переживем)
                throw new ArgumentException("Max temperature cannot be less than min temperature");
        }

        public string DateDisplay => Date.ToString("dd.MM (ddd)");
        public string FormattedTemperatureRange => $"{MinTemperature.Celsius:F0}° / {MaxTemperature.Celsius:F0}°C";
    }
}
