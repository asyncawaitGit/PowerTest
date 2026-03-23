using System;

namespace WeatherPowerTest.Domain.ValueObjects
{
    /// <summary>
    /// Температура
    /// </summary>
    public class Temperature : IEquatable<Temperature>
    {
        public double Celsius { get; private set; }
        public double Fahrenheit { get; private set; }

        public Temperature(double celsius)
        {
            Celsius = celsius;
            Fahrenheit = celsius * 9 / 5 + 32;
        }

        public static Temperature FromCelsius(double celsius)
        {
            return new Temperature(celsius);
        }

        public static Temperature FromFahrenheit(double fahrenheit)
        {
            return new Temperature((fahrenheit - 32) * 5 / 9);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Temperature);
        }

        public bool Equals(Temperature other)
        {
            if (other is null) return false;
            return Math.Abs(Celsius - other.Celsius) < 0.01;
        }

        public override int GetHashCode()
        {
            return Celsius.GetHashCode();
        }

        public static bool operator ==(Temperature left, Temperature right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(Temperature left, Temperature right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{Celsius:F1}°C ({Fahrenheit:F1}°F)";
        }
    }
}
