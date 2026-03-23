using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherPowerTest.Application.Interfaces;
using WeatherPowerTest.Application.Responses;
using WeatherPowerTest.Infrastructure.Configuration;

namespace WeatherPowerTest.Infrastructure.ApiClients
{

    /// <summary>
    /// Реализация клиента для работы с WeatherAPI
    /// </summary>
    public class WeatherApiClient : IWeatherApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherApiSettings _settings;

        public WeatherApiClient(HttpClient httpClient, WeatherApiSettings settings)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            _httpClient.Timeout = TimeSpan.FromSeconds(_settings.TimeoutSeconds);
        }

        public async Task<WeatherApiResponse> GetCurrentWeatherAsync()
        {
            try
            {
                var url = _settings.GetCurrentWeatherUrl();
                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return WeatherApiResponse.Success(json);
                }

                return WeatherApiResponse.Fail(
                    $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}",
                    (int)response.StatusCode);
            }
            catch (TaskCanceledException ex)
            {
                return WeatherApiResponse.Fail($"Timeout: {ex.Message}");
            }
            catch (HttpRequestException ex)
            {
                return WeatherApiResponse.Fail($"Network error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return WeatherApiResponse.Fail($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<WeatherApiResponse> GetForecastAsync(int days)
        {
            try
            {
                var url = _settings.GetForecastUrl(days);
                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return WeatherApiResponse.Success(json);
                }

                return WeatherApiResponse.Fail(
                    $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}",
                    (int)response.StatusCode);
            }
            catch (TaskCanceledException ex)
            {
                return WeatherApiResponse.Fail($"Timeout: {ex.Message}");
            }
            catch (HttpRequestException ex)
            {
                return WeatherApiResponse.Fail($"Network error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return WeatherApiResponse.Fail($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<WeatherPowerTest.Application.DTOs.LocationDto> GetLocationAsync()
        {
            try
            {
                var url = _settings.GetCurrentWeatherUrl();
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var location = Newtonsoft.Json.Linq.JObject.Parse(json)["location"];

                if (location == null)
                    return null;

                return new WeatherPowerTest.Application.DTOs.LocationDto
                {
                    Name = (string)location["name"],
                    Country = (string)location["country"],
                    Latitude = (double)location["lat"],
                    Longitude = (double)location["lon"],
                    LocalTime = (string)location["localtime"]
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
