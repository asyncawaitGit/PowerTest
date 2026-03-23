using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPowerTest.Domain.Constants
{
    /// <summary>
    /// Константы для работы с API
    /// </summary>
    public static class ApiConstants
    {
        public const string WeatherApiBaseUrl = "http://api.weatherapi.com/v1/";
        public const string CurrentWeatherEndpoint = "current.json";
        public const string ForecastEndpoint = "forecast.json";

        // Координаты Москвы ызял из нейросети
        public const double MoscowLatitude = 55.7558;
        public const double MoscowLongitude = 37.6173;

        // Параметры по умолчанию
        public const int DefaultForecastDays = 3;
        public const int CacheMaxAgeMinutes = 30;

        // Много, для такого сервиса, но все же)
        public const int HttpTimeoutSeconds = 30;

        // Коды ошибок
        public const string ErrorCodeApiUnavailable = "API_UNAVAILABLE";
        public const string ErrorCodeInvalidData = "INVALID_DATA";
        public const string ErrorCodeLocationNotFound = "LOCATION_NOT_FOUND";
    }
}
