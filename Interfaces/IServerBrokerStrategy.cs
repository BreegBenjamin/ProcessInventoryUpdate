using Inventory.Shared.Model;

namespace ProcessInventoryUpdate.Interfaces
{
    public interface IServerBrokerStrategy
    {
        Task<bool> SendMessage(ProccessInventoryModel proccessInventoryModel);
    }
}
