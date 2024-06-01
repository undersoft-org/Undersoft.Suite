namespace Undersoft.SDK.Service.Data.Event.Provider.RabbitMq
{
    public interface IChannelPool : IDisposable
    {
        IChannelAccessor Acquire(string? channelName = null, string? connectionName = null);
    }
}