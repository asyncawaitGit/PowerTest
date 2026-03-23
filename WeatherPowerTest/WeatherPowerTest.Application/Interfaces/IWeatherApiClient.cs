using System.Threading.Tasks;
using WeatherPowerTest.Application.DTOs;
using WeatherPowerTest.Application.Responses;

namespace WeatherPowerTest.Application.Interfaces
{
    /// <summary>
    /// Клиент для работы с внешним API погоды
    /// </summary>
    public interface IWeatherApiClient
    {
        /// <summary>
        /// Получить сырой ответ API для текущей погоды
        /// </summary>
        Task<WeatherApiResponse> GetCurrentWeatherAsync();

        /// <summary>
        /// Получить сырой ответ API для прогноза
        /// </summary>
        Task<WeatherApiResponse> GetForecastAsync(int days);

        /// <summary>
        /// Получить локацию (город)
        /// </summary>
        Task<LocationDto> GetLocationAsync();
    }
}
