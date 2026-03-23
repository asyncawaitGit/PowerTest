using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPowerTest.Application.Responses
{
    /// <summary>
    /// Ответ от API
    /// </summary>
    public class WeatherApiResponse
    {
        public string RawJson { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public int? StatusCode { get; set; }

        public static WeatherApiResponse Success(string json)
        {
            return new WeatherApiResponse
            {
                RawJson = json,
                IsSuccess = true
            };
        }

        public static WeatherApiResponse Fail(string error, int? statusCode = null)
        {
            return new WeatherApiResponse
            {
                IsSuccess = false,
                ErrorMessage = error,
                StatusCode = statusCode
            };
        }
    }
}
