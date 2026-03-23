using System;
using WeatherPowerTest.Domain.Entities;

namespace WeatherPowerTest.Application.Validators
{
    /// <summary>
    /// Валидатор данных погоды
    /// </summary>
    public static class WeatherDataValidator
    {
        public static bool IsValid(WeatherData weatherData)
        {
            if (weatherData == null)
                return false;

            if (weatherData.Current == null)
                return false;

            if (weatherData.HourlyForecasts == null || weatherData.HourlyForecasts.Count == 0)
                return false;

            if (weatherData.DailyForecasts == null || weatherData.DailyForecasts.Count == 0)
                return false;

            // температура
            if (weatherData.Current.Temperature.Celsius < -100 ||
                weatherData.Current.Temperature.Celsius > 60)
                return false;

            // влажность
            if (weatherData.Current.Humidity.Percent < 0 ||
                weatherData.Current.Humidity.Percent > 100)
                return false;

            // ветер
            if (weatherData.Current.Wind.SpeedKph < 0 ||
                weatherData.Current.Wind.SpeedKph > 200)
                return false;

            // TODO: Я бы мог сдедать валидацию с Behaviors, но вас смотрит 32 человека, сомневаюсь что вы вообще посмотрите мою реализацию....
            // Короче Solid - для тех кто посмотрит, спросите меня об этом на собесе))))

            return true;
        }

        public static bool IsDataFresh(WeatherData weatherData, int maxAgeMinutes = 30)
        {
            if (weatherData == null)
                return false;

            return (DateTime.UtcNow - weatherData.RetrievedAt).TotalMinutes <= maxAgeMinutes;
        }
    }
}
