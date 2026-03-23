using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPowerTest.Application.DTOs;
using WeatherPowerTest.Application.Mappings;
using WeatherPowerTest.Domain.Interfaces;

namespace WeatherPowerTest.Application.Queries
{
    /// <summary>
    /// Запрос для получения погоды
    /// </summary>
    public class GetWeatherQuery
    {
        private readonly IWeatherService _weatherService;

        public GetWeatherQuery(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<WeatherDataDto> ExecuteAsync()
        {
            var weatherData = await _weatherService.GetWeatherDataAsync();
            return weatherData.ToDto();
        }
    }
}
