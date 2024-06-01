using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using RabbitMQ.Client;

namespace Undersoft.SDK.Service.Data.Event.Provider.RabbitMq
{
    public class QueueDeclareConfiguration
    {
        [DisallowNull] public string QueueName { get; }

        public bool Durable { get; set; }

        public bool Exclusive { get; set; }

        public bool AutoDelete { get; set; }

        public ushort? PrefetchCount { get; set; }

        public IDictionary<string, object> Arguments { get; }

        public QueueDeclareConfiguration(
            [DisallowNull] string queueName,
            bool durable = true,
            bool exclusive = false,
            bool autoDelete = false,
            ushort? prefetchCount = null)
        {
            QueueName = queueName;
            Durable = durable;
            Exclusive = exclusive;
            AutoDelete = autoDelete;
            Arguments = new Dictionary<string, object>();
            PrefetchCount = prefetchCount;
        }

        public virtual QueueDeclareOk Declare(IModel channel)
        {
            return channel.QueueDeclare(
                queue: QueueName,
                durable: Durable,
                exclusive: Exclusive,
                autoDelete: AutoDelete,
                arguments: Arguments
            );
        }
    }
}