using CustomLogger.Abstraction;
using CustomLogger.Model;
using Microsoft.Extensions.DependencyInjection;

namespace CustomLogger.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddCustomLogger(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ICustomLogger<>), typeof(CustomLoggerClass<>));
            return services;
        }   
    }
}
