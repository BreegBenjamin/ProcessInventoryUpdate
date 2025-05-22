using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProcessInventoryUpdate.Interfaces;
using ProcessInventoryUpdate.Model;
using System.Text.Json;

namespace ProcessInventoryUpdate.Services
{
    public class AzureServiceBusStrategy : IServerBrokerStrategy
    {
        private readonly ServiceBusSender _sender;
        private readonly ILogger<ProcessInventory> _logger;

        public AzureServiceBusStrategy(IConfiguration configuration, ILogger<ProcessInventory> logger)
        {
            _logger = logger;
            // Get the connection string and queue name from configuration
            string connectionString = configuration.GetValue<string>("serviceBusConnectionString")!;
            string queueName = configuration.GetValue<string>("queueName")!;

            // Create a Service Bus client and processor
            var client = new ServiceBusClient(connectionString);
            _sender = client.CreateSender(queueName);
        }
        public async Task<bool> SendMessage(ProccessInventoryModel model)
        {
            try
            {
                string jsonBody = JsonSerializer.Serialize(model);
                var message = new ServiceBusMessage(jsonBody);

                await _sender.SendMessageAsync(message);

                _logger.LogInformation("Mensaje enviado a Service Bus correctamente.");
                return true;    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar mensaje a Service Bus.");
                return false;
            }

        }
    }
}
