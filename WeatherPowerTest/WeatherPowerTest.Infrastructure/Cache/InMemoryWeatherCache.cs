using System;
using System.Threading.Tasks;
using WeatherPowerTest.Application.Interfaces;
using WeatherPowerTest.Domain.Entities;

namespace WeatherPowerTest.Infrastructure.Cache
{
    /// <summary>
    /// In-Memory реализация кэша для данных погоды
    /// </summary>
    public class InMemoryWeatherCache : IWeatherCache
    {
        private WeatherData _cachedData;
        private DateTime _cachedAt;
        private readonly object _lockObject = new object();
        private readonly int _cacheDurationMinutes;

        public InMemoryWeatherCache(int cacheDurationMinutes = 30)
        {
            _cacheDurationMinutes = cacheDurationMinutes;
        }

        public Task<WeatherData> GetAsync()
        {
            lock (_lockObject)
            {
                return Task.FromResult(_cachedData);
            }
        }

        public Task SetAsync(WeatherData weatherData)
        {
            lock (_lockObject)
            {
                _cachedData = weatherData;
                _cachedAt = DateTime.UtcNow;
            }
            return Task.CompletedTask;
        }

        public Task<bool> IsValidAsync(int maxAgeMinutes = 30)
        {
            lock (_lockObject)
            {
                if (_cachedData == null)
                    return Task.FromResult(false);

                var isValid = (DateTime.UtcNow - _cachedAt).TotalMinutes <= maxAgeMinutes;
                return Task.FromResult(isValid);
            }
        }

        public Task ClearAsync()
        {
            lock (_lockObject)
            {
                _cachedData = null;
                _cachedAt = DateTime.MinValue;
            }
            return Task.CompletedTask;
        }
    }
}