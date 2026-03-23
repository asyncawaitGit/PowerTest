using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPowerTest.Domain.Exceptions
{
    /// <summary>
    /// Исключение при ошибке валидации данных
    /// </summary>
    public class WeatherValidationException : WeatherException
    {
        public string PropertyName { get; }

        public WeatherValidationException(string message, string propertyName)
            : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
