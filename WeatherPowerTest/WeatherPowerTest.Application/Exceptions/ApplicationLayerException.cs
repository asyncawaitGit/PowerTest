using System;

namespace WeatherPowerTest.Application.Exceptions
{
    /// <summary>
    /// Базовое исключение для Application слоя
    /// </summary>
    public class ApplicationLayerException : Exception
    {
        public ApplicationLayerException(string message) : base(message) { }

        public ApplicationLayerException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
