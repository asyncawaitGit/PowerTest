using System;

namespace WeatherPowerTest.Infrastructure.Exceptions
{
    /// <summary>
    /// Исключение при ошибке кэширования
    /// </summary>
    public class CacheException : InfrastructureException
    {
        public string CacheKey { get; }

        public CacheException(string message, string cacheKey, Exception innerException)
            : base(message, innerException)
        {
            CacheKey = cacheKey;
        }
    }
}
