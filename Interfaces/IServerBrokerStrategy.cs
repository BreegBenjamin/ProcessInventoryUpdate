using ProcessInventoryUpdate.Model;

namespace ProcessInventoryUpdate.Interfaces
{
    public interface IServerBrokerStrategy
    {
        Task<bool> SendMessage(ProccessInventoryModel proccessInventoryModel);
    }
}
