using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProcessInventoryUpdate.Interfaces;
using ProcessInventoryUpdate.Services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddScoped<AzureServiceBusStrategy>();
builder.Services.AddScoped<RabbitMQStrategy>();
builder.Services.AddScoped<IServiceBrokerStrategyFactory, ServiceBrokerStrategyFactory>(); 

builder.Build().Run();