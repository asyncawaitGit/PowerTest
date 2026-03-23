using System;

namespace WeatherPowerTest.Application.Exceptions
{
    /// <summary>
    /// Исключение при ошибке кэширования
    /// </summary>
    public class CacheException : ApplicationLayerException
    {
        public CacheException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
