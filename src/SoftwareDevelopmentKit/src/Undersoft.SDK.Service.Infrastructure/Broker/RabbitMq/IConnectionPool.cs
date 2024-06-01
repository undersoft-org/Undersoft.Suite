using System;
using RabbitMQ.Client;

namespace Undersoft.SDK.Service.Data.Event.Provider.RabbitMq
{
    public interface IConnectionPool : IDisposable
    {
        IConnection Get(string connectionName = null);
    }
}