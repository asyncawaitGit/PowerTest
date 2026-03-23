namespace WeatherPowerTest.Application.DTOs
{
    public class HourlyForecastDto
    {
        public string Time { get; set; }
        public double TempC { get; set; }
        public string ConditionText { get; set; }
        public string ConditionIcon { get; set; }
        public double ChanceOfRain { get; set; }
    }
}
