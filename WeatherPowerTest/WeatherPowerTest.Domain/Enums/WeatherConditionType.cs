using System.ComponentModel;

namespace WeatherPowerTest.Domain.Enums
{
    /// <summary>
    /// Типы погодных условий
    /// </summary>
    public enum WeatherConditionType
    {
        [Description("Ясно")]
        Clear,

        [Description("Облачно")]
        Cloudy,

        [Description("Пасмурно")]
        Overcast,

        [Description("Дождь")]
        Rain,

        [Description("Снег")]
        Snow,

        [Description("Гроза")]
        Thunderstorm,

        [Description("Туман")]
        Fog,

        [Description("Неизвестно")]
        Unknown
    }
}
