using System;

namespace WeatherPowerTest.Domain.Exceptions
{
    /// <summary>
    /// Базовое исключение погоды
    /// </summary>
    public class WeatherException : Exception
    {
        public WeatherException(string message) : base(message) { }

        public WeatherException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
