using System.Threading.Tasks;
using WeatherPowerTest.Domain.Entities;

namespace WeatherPowerTest.Application.Interfaces
{
    /// <summary>
    /// Кэш для данных погоды
    /// </summary>
    public interface IWeatherCache
    {
        /// <summary>
        /// Получить данные из кэша
        /// </summary>
        Task<WeatherData> GetAsync();

        /// <summary>
        /// Сохранить данные в кэш
        /// </summary>
        Task SetAsync(WeatherData weatherData);

        /// <summary>
        /// Проверить валидность кэша
        /// </summary>
        Task<bool> IsValidAsync(int maxAgeMinutes = 30);

        /// <summary>
        /// Очистить кэш
        /// </summary>
        Task ClearAsync();
    }
}
