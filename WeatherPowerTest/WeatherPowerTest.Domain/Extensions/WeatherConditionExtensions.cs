using System.ComponentModel;
using System.Linq;
using WeatherPowerTest.Domain.Enums;

namespace WeatherPowerTest.Domain.Extensions
{
    public static class WeatherConditionExtensions
    {
        public static WeatherConditionType ParseFromString(string conditionText)
        {
            if (string.IsNullOrWhiteSpace(conditionText))
                return WeatherConditionType.Unknown;

            conditionText = conditionText.ToLowerInvariant();

            if (conditionText.Contains("sunny") || conditionText.Contains("clear"))
                return WeatherConditionType.Clear;

            if (conditionText.Contains("partly cloudy") || conditionText.Contains("cloudy"))
                return WeatherConditionType.Cloudy;

            if (conditionText.Contains("overcast"))
                return WeatherConditionType.Overcast;

            if (conditionText.Contains("rain") || conditionText.Contains("drizzle"))
                return WeatherConditionType.Rain;

            if (conditionText.Contains("snow"))
                return WeatherConditionType.Snow;

            if (conditionText.Contains("thunder") || conditionText.Contains("storm"))
                return WeatherConditionType.Thunderstorm;

            if (conditionText.Contains("fog") || conditionText.Contains("mist"))
                return WeatherConditionType.Fog;

            return WeatherConditionType.Unknown;
        }

        public static string GetDescription(this WeatherConditionType type)
        {
            var field = type.GetType().GetField(type.ToString());
            var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute;
            return attribute?.Description ?? type.ToString();
        }
    }
}
