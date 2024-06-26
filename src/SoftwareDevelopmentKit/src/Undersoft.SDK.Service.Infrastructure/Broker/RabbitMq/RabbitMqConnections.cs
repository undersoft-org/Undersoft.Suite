﻿using RabbitMQ.Client;
using System.Diagnostics.CodeAnalysis;

namespace Undersoft.SDK.Service.Data.Event.Provider.RabbitMq
{
    [Serializable]
    public class RabbitMqConnections : Registry<ConnectionFactory>
    {
        public const string DefaultConnectionName = "Default";

        [DisallowNull]
        public ConnectionFactory Default
        {
            get => this[DefaultConnectionName];
            set => this[DefaultConnectionName] = value;
        }

        public RabbitMqConnections()
        {
            Default = new ConnectionFactory();
        }

        public ConnectionFactory GetOrDefault(string connectionName)
        {
            if (TryGet(connectionName, out ConnectionFactory connectionFactory))
            {
                return connectionFactory;
            }

            return Default;
        }
    }
}