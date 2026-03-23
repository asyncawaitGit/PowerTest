using System;
using Microsoft.Extensions.DependencyInjection;
using WeatherPowerTest.Application.Interfaces;
using WeatherPowerTest.Application.Services;
using WeatherPowerTest.Domain.Interfaces;
using WeatherPowerTest.Infrastructure.ApiClients;
using WeatherPowerTest.Infrastructure.Cache;
using WeatherPowerTest.Infrastructure.Configuration;
using WeatherPowerTest.Infrastructure.Factories;

namespace WeatherPowerTest.Infrastructure.Extensions
{
    /// <summary>
    /// Расширения для DI контейнера
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeatherInfrastructure(
            this IServiceCollection services,
            Action<WeatherApiSettings> configureSettings = null)
        {
            // Настройки
            var settings = new WeatherApiSettings();
            configureSettings?.Invoke(settings);

            // Валидация настроек
            if (string.IsNullOrEmpty(settings.ApiKey))
                throw new InvalidOperationException("API Key не установлен");

            if (settings.Latitude == 0 || settings.Longitude == 0)
                throw new InvalidOperationException("Координаты не установлены");

            services.AddSingleton(settings);

            // HttpClient (как Singleton для эффективности)
            services.AddSingleton(sp => HttpClientFactory.Create(settings));

            // Infrastructure сервисы
            services.AddScoped<IWeatherApiClient, WeatherApiClient>();
            services.AddScoped<IWeatherCache, InMemoryWeatherCache>();

            // Application сервисы
            services.AddScoped<IWeatherService, WeatherService>();

            return services;
        }

        public static IServiceCollection AddWeatherInfrastructureWithCustomCache(
            this IServiceCollection services,
            IWeatherCache customCache,
            Action<WeatherApiSettings> configureSettings = null)
        {
            // Настройки
            var settings = new WeatherApiSettings();
            configureSettings?.Invoke(settings);

            services.AddSingleton(settings);
            services.AddSingleton(sp => HttpClientFactory.Create(settings));
            services.AddScoped<IWeatherApiClient, WeatherApiClient>();
            services.AddScoped(_ => customCache);
            services.AddScoped<IWeatherService, WeatherService>();

            return services;
        }
    }
}