using ProcessInventoryUpdate.Interfaces;
using Inventory.Shared.Model;

namespace ProcessInventoryUpdate.Services
{
    public class RabbitMQStrategy : IServerBrokerStrategy
    {
        public Task<bool> SendMessage(ProccessInventoryModel proccessInventoryModel)
        {
            throw new NotImplementedException();
        }
    } 
}
