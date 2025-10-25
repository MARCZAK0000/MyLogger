using CustomLogger.Abstraction;
using CustomLogger.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace CustomLogger.Model
{
    public sealed class CustomLoggerFactory : ICustomLoggerFactory
    {
        public CustomLoggerFactory()
        {
            
        }

        public ICustomLogger<T> CreateLogger<T>()
            where T : class
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCustomLogger();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider.GetRequiredService<ICustomLogger<T>>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
