using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherPowerTest.Domain.Entities;

namespace WeatherPowerTest.Domain.Interfaces
{
    /// <summary>
    /// Сервис погоды
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Получить данные о погоде
        /// </summary>
        Task<WeatherData> GetWeatherDataAsync();

        /// <summary>
        /// Получить только текущую погоду
        /// </summary>
        Task<CurrentWeather> GetCurrentWeatherAsync();

        /// <summary>
        /// Получить почасовой прогноз
        /// </summary>
        Task<IReadOnlyList<HourlyForecast>> GetHourlyForecastAsync();

        /// <summary>
        /// Получить дневной прогноз
        /// </summary>
        Task<IReadOnlyList<DailyForecast>> GetDailyForecastAsync(int days = 3);
    }
}
