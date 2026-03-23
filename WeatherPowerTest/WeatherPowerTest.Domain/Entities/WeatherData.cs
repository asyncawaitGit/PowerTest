using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPowerTest.Domain.Entities
{
    /// <summary>
    /// Полные данные о погоде
    /// </summary>
    public class WeatherData
    {
        public CurrentWeather Current { get; private set; }
        public IReadOnlyList<HourlyForecast> HourlyForecasts { get; private set; }
        public IReadOnlyList<DailyForecast> DailyForecasts { get; private set; }
        public DateTime RetrievedAt { get; private set; }

        public WeatherData(
            CurrentWeather current,
            IReadOnlyList<HourlyForecast> hourly,
            IReadOnlyList<DailyForecast> daily)
        {
            Current = current ?? throw new ArgumentNullException(nameof(current));
            HourlyForecasts = hourly ?? throw new ArgumentNullException(nameof(hourly));
            DailyForecasts = daily ?? throw new ArgumentNullException(nameof(daily));
            RetrievedAt = DateTime.UtcNow;
        }

        public bool IsDataStale(int maxAgeMinutes = 30)
        {
            return (DateTime.UtcNow - RetrievedAt).TotalMinutes > maxAgeMinutes;
        }
    }
}
