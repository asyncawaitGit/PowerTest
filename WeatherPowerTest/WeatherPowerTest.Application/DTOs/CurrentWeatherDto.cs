namespace WeatherPowerTest.Application.DTOs
{
    public class CurrentWeatherDto
    {
        public double TempC { get; set; }
        public double TempF { get; set; }
        public string ConditionText { get; set; }
        public string ConditionIcon { get; set; }
        public double WindKph { get; set; }
        public int Humidity { get; set; }
        public string LastUpdated { get; set; }
        public string Location { get; set; }
    }
}
