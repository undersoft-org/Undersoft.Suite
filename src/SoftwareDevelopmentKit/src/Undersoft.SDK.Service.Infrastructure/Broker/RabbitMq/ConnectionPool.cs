using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Undersoft.SDK.Service.Data.Event.Provider.RabbitMq
{
    public class ConnectionPool : IConnectionPool
    {
        protected RabbitMqOptions Options { get; }

        protected ConcurrentDictionary<string, Lazy<IConnection>> Connections { get; }

        private bool _isDisposed;

        public ConnectionPool(IOptions<RabbitMqOptions> options)
        {
            Options = options.Value;
            Connections = new ConcurrentDictionary<string, Lazy<IConnection>>();
        }

        public virtual IConnection Get(string connectionName = null)
        {
            connectionName ??= RabbitMqConnections.DefaultConnectionName;

            try
            {
                var lazyConnection = Connections.GetOrAdd(
                    connectionName, (s) =>
                         new Lazy<IConnection>(
                             () =>
                    {
                        var connection = Options.Connections.GetOrDefault(connectionName);
                        var hostnames = connection.HostName.TrimEnd(';').Split(';');

                        return hostnames.Length == 1 ? connection.CreateConnection() : connection.CreateConnection(hostnames);

                    }));


                return lazyConnection.Value;
            }
            catch (Exception)
            {
                Connections.TryRemove(connectionName, out _);
                throw;
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            foreach (var connection in Connections.Values)
            {
                try
                {
                    connection.Value.Dispose();
                }
                catch
                {

                }
            }

            Connections.Clear();
        }
    }
}