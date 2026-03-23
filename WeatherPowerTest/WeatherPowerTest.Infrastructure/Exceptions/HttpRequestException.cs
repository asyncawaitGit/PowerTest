namespace WeatherPowerTest.Infrastructure.Exceptions
{
    /// <summary>
    /// Исключение при ошибке HTTP запроса
    /// </summary>
    public class HttpRequestException : InfrastructureException
    {
        public string Url { get; }
        public int? StatusCode { get; }

        public HttpRequestException(string message, string url, int? statusCode = null)
            : base(message)
        {
            Url = url;
            StatusCode = statusCode;
        }
    }
}
