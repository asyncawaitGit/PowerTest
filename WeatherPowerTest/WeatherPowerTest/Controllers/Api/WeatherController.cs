using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WeatherPowerTest.Application.Mappings;
using WeatherPowerTest.Domain.Interfaces;
using WeatherPowerTest.Helpers;

namespace WeatherPowerTest.Controllers.Api
{
    /// <summary>
    /// API контроллер для получения данных о погоде
    /// </summary>
    public class WeatherController : ApiController
    {
        private readonly IWeatherService _weatherService;

        public WeatherController()
        {
            // Получаем сервис из DI контейнера
            _weatherService = DependencyResolverHelper.GetService<IWeatherService>();
        }

        /// <summary>
        /// Получить полные данные о погоде
        /// </summary>
        [HttpGet]
        public async Task<HttpResponseMessage> GetWeather()
        {
            try
            {
                var weatherData = await _weatherService.GetWeatherDataAsync();
                var dto = weatherData.ToDto();

                return Request.CreateResponse(HttpStatusCode.OK, dto);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError,
                    new HttpError($"Ошибка получения данных: {ex.Message}")
                );
            }
        }

        /// <summary>
        /// Получить только текущую погоду
        /// </summary>
        [HttpGet]
        public async Task<HttpResponseMessage> GetCurrent()
        {
            try
            {
                var current = await _weatherService.GetCurrentWeatherAsync();
                var dto = current.ToDto(); // Теперь работает!

                return Request.CreateResponse(HttpStatusCode.OK, dto);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError,
                    new HttpError($"Ошибка получения данных: {ex.Message}")
                );
            }
        }

        /// <summary>
        /// Получить почасовой прогноз
        /// </summary>
        [HttpGet]
        public async Task<HttpResponseMessage> GetHourly()
        {
            try
            {
                var hourly = await _weatherService.GetHourlyForecastAsync();
                var dto = hourly.ToDtoList(); // Теперь работает!

                return Request.CreateResponse(HttpStatusCode.OK, dto);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError,
                    new HttpError($"Ошибка получения данных: {ex.Message}")
                );
            }
        }

        /// <summary>
        /// Получить дневной прогноз
        /// </summary>
        [HttpGet]
        public async Task<HttpResponseMessage> GetForecast(int days = 3)
        {
            try
            {
                var forecast = await _weatherService.GetDailyForecastAsync(days);
                var dto = forecast.ToDtoList(); // Теперь работает!

                return Request.CreateResponse(HttpStatusCode.OK, dto);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError,
                    new HttpError($"Ошибка получения данных: {ex.Message}")
                );
            }
        }
    }
}