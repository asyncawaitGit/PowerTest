using System.Net.Http;
using WeatherPowerTest.Infrastructure.Configuration;

namespace WeatherPowerTest.Infrastructure.Factories
{

    /// <summary>
    /// Фабрика для создания HttpClient
    /// </summary>
    public static class HttpClientFactory
    {
        public static HttpClient Create(WeatherApiSettings settings)
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = System.TimeSpan.FromSeconds(settings.TimeoutSeconds);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "WeatherPowerTest/1.0");

            return httpClient;
        }
    }
}
