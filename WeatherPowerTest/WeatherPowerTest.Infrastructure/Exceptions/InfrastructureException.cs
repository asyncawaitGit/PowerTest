using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPowerTest.Infrastructure.Exceptions
{
    /// <summary>
    /// Базовое исключение
    /// </summary>
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string message) : base(message) { }

        public InfrastructureException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
