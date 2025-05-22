using ProcessInventoryUpdate.Interfaces;
using ProcessInventoryUpdate.Model;

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
