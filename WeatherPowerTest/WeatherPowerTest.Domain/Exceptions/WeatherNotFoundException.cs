using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPowerTest.Domain.Exceptions
{
    /// <summary>
    /// Исключение при отсутствии данных
    /// </summary>
    public class WeatherNotFoundException : WeatherException
    {
        public string Location { get; }

        public WeatherNotFoundException(string message, string location)
            : base(message)
        {
            Location = location;
        }
    }
}
