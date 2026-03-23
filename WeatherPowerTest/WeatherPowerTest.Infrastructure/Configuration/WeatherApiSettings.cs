namespace WeatherPowerTest.Infrastructure.Configuration
{
    /// <summary>
    /// Настройки для API погоды
    /// </summary>
    public class WeatherApiSettings
    {
        public string ApiKey { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string BaseUrl { get; set; }
        public int TimeoutSeconds { get; set; }
        public int MaxRetryCount { get; set; }

        public WeatherApiSettings()
        {
            ApiKey = "fa8b3df74d4042b9aa7135114252304";

            Latitude = 55.7558;   // Москва
            Longitude = 37.6173;  // Москва

            BaseUrl = "http://api.weatherapi.com/v1/";

            TimeoutSeconds = 30;
            MaxRetryCount = 3;
        }

        public string GetLocationString()
        {
            return $"{Latitude},{Longitude}";
        }

        public string GetCurrentWeatherUrl()
        {
            return $"{BaseUrl}current.json?key={ApiKey}&q={GetLocationString()}";
        }

        public string GetForecastUrl(int days)
        {
            return $"{BaseUrl}forecast.json?key={ApiKey}&q={GetLocationString()}&days={days}";
        }
    }
}
