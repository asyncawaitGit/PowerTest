namespace WeatherPowerTest.Application.Exceptions
{
    /// <summary>
    /// Исключение при ошибке валидации в Application слое
    /// </summary>
    public class ValidationException : ApplicationLayerException
    {
        public ValidationException(string message) : base(message) { }
    }
}
