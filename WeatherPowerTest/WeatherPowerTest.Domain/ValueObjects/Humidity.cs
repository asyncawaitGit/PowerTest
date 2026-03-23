using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPowerTest.Domain.ValueObjects
{
    /// <summary>
    /// Влажность
    /// </summary>
    public class Humidity : IEquatable<Humidity>
    {
        public int Percent { get; private set; }

        public Humidity(int percent)
        {
            if (percent < 0 || percent > 100)
                throw new ArgumentException("Humidity must be between 0 and 100", nameof(percent));

            Percent = percent;
        }

        public static Humidity FromPercent(int percent)
        {
            return new Humidity(percent);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Humidity);
        }

        public bool Equals(Humidity other)
        {
            if (other is null) return false;
            return Percent == other.Percent;
        }

        public override int GetHashCode()
        {
            return Percent.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Percent}%";
        }
    }
}
