using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WeatherPowerTest.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherPowerTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ConfigureServices();
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddWeatherInfrastructure(settings =>
            {
                settings.ApiKey = "fa8b3df74d4042b9aa7135114252304";

                // Координаты взял из нейронки
                settings.Latitude = 55.7558;
                settings.Longitude = 37.6173;

                settings.BaseUrl = "http://api.weatherapi.com/v1/";

                settings.TimeoutSeconds = 30;
                settings.MaxRetryCount = 3;
            });

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
