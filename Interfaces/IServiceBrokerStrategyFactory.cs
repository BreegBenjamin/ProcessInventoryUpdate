namespace ProcessInventoryUpdate.Interfaces
{
    public interface IServiceBrokerStrategyFactory
    {
        IServerBrokerStrategy Create(string authType);
    }
}
