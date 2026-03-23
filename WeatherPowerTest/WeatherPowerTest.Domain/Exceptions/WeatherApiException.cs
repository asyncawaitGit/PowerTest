using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPowerTest.Domain.Exceptions
{
    /// <summary>
    /// Исключение при ошибке API
    /// </summary>
    public class WeatherApiException : WeatherException
    {
        public string ApiEndpoint { get; }
        public int? StatusCode { get; }

        public WeatherApiException(string message, string apiEndpoint, int? statusCode = null)
            : base(message)
        {
            ApiEndpoint = apiEndpoint;
            StatusCode = statusCode;
        }
    }
}
