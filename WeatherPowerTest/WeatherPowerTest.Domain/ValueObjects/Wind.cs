using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPowerTest.Domain.ValueObjects
{
    /// <summary>
    /// Ветер
    /// </summary>
    public class Wind : IEquatable<Wind>
    {
        public double SpeedKph { get; private set; }
        public double SpeedMph { get; private set; }

        public Wind(double speedKph)
        {
            if (speedKph < 0)
                throw new ArgumentException("Wind speed cannot be negative", nameof(speedKph));

            SpeedKph = speedKph;
            SpeedMph = speedKph / 1.60934;
        }

        public static Wind FromKph(double kph)
        {
            return new Wind(kph);
        }

        public static Wind FromMph(double mph)
        {
            return new Wind(mph * 1.60934);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Wind);
        }

        public bool Equals(Wind other)
        {
            if (other is null) return false;
            return Math.Abs(SpeedKph - other.SpeedKph) < 0.01;
        }

        public override int GetHashCode()
        {
            return SpeedKph.GetHashCode();
        }

        public override string ToString()
        {
            return $"{SpeedKph:F1} км/ч";
        }
    }
}
