using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProcessInventoryUpdate.Interfaces;
using Inventory.Shared.Model;
using System.Text.Json;

namespace ProcessInventoryUpdate
{
    public class ProcessInventory
    {
        private readonly ILogger<ProcessInventory> _logger;
        private readonly string strategy;
        private readonly IServiceBrokerStrategyFactory _serviceBrokerStrategyFactory;

        public ProcessInventory(ILogger<ProcessInventory> logger, IConfiguration configuration, IServiceBrokerStrategyFactory serviceBrokerStrategyFactory)
        {
            _logger = logger;
            _serviceBrokerStrategyFactory = serviceBrokerStrategyFactory;
            strategy = configuration.GetValue<string>("ServiceBrokerStrategy")!;
        }

        [Function("ProcessInventoryUpdate")]
        public async Task<IActionResult> Run( [HttpTrigger(AuthorizationLevel.Function,"post")]  HttpRequest req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var request = JsonSerializer.Deserialize<ProccessInventoryModel>(requestBody, options);

            if (request == null)
            {
                _logger.LogError("Invalid request body."); 
                return new BadRequestObjectResult(new
                {
                    Status = false,
                    Message = "Invalid request body."
                });
            }

            var serviceBus = _serviceBrokerStrategyFactory.Create(strategy);

            _logger.LogInformation($"Sending message to service buss id: {request.productId}");

            bool result = await serviceBus.SendMessage(request);
            string msResponse = (result) ? "Message send Correct" : "Fail to delivery the message";
            var response = new 
            {
                Status = result,
                Message = msResponse
            };

            return new OkObjectResult(response);
        }
    }
}
