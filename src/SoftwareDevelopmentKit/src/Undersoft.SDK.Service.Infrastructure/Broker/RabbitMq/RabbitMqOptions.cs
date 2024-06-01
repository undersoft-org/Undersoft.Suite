namespace Undersoft.SDK.Service.Data.Event.Provider.RabbitMq
{
    public class RabbitMqOptions
    {
        public RabbitMqConnections Connections { get; }

        public RabbitMqOptions()
        {
            Connections = new RabbitMqConnections();
        }
    }
}