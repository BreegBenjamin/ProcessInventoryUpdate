using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProcessInventoryUpdate.Interfaces;

namespace ProcessInventoryUpdate.Services
{
    public class ServiceBrokerStrategyFactory : IServiceBrokerStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ServiceBrokerStrategyFactory> _logger;
        public ServiceBrokerStrategyFactory(IServiceProvider serviceProvider, ILogger<ServiceBrokerStrategyFactory> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public IServerBrokerStrategy Create(string serviceBroker)
        {
            try
            {
                return serviceBroker switch
                {
                    "azure" => _serviceProvider.GetRequiredService<AzureServiceBusStrategy>(),
                    "rabbit" => _serviceProvider.GetRequiredService<RabbitMQStrategy>(),
                    _ => throw new NotSupportedException($"El tipo de autenticación '{serviceBroker}' no está soportado.")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear la estrategia de servicio: {ex.Message}");
                throw;
            }
        }
    } 
}
