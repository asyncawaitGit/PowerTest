using System;

namespace WeatherPowerTest.Helpers
{
    public static class DependencyResolverHelper
    {
        public static T GetService<T>()
        {
            var serviceProvider = MvcApplication.ServiceProvider;
            if (serviceProvider == null)
                throw new InvalidOperationException("ServiceProvider не инициализирован");

            var service = serviceProvider.GetService(typeof(T));
            if (service == null)
                throw new InvalidOperationException($"Сервис {typeof(T).Name} не зарегистрирован");

            return (T)service;
        }
    }
}