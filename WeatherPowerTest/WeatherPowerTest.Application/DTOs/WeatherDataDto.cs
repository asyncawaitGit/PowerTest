using System;
using System.Collections.Generic;

namespace WeatherPowerTest.Application.DTOs
{
    public class WeatherDataDto
    {
        public CurrentWeatherDto Current { get; set; }
        public List<HourlyForecastDto> Hourly { get; set; }
        public List<DailyForecastDto> Forecast { get; set; }
        public DateTime RetrievedAt { get; set; }
        public bool IsFromCache { get; set; }
    }
}
