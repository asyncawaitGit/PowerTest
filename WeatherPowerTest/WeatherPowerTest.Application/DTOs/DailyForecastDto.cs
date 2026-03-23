namespace WeatherPowerTest.Application.DTOs
{
    public class DailyForecastDto
    {
        public string Date { get; set; }
        public double MaxTempC { get; set; }
        public double MinTempC { get; set; }
        public string ConditionText { get; set; }
        public string ConditionIcon { get; set; }
    }
}
