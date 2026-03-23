using System.Threading.Tasks;
using WeatherPowerTest.Domain.Entities;

namespace WeatherPowerTest.Domain.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с кэшем погоды
    /// </summary>
    public interface IWeatherRepository
    {
        Task<WeatherData> GetCachedWeatherAsync();


        Task SaveWeatherAsync(WeatherData weather);


        Task<bool> IsCacheValidAsync(int maxAgeMinutes = 30);


        void ClearCache();
    }
}
