using System;

namespace Undersoft.SDK.Service.Data.Event.Provider.RabbitMq
{
    public interface IRabbitMqSerializer
    {
        byte[] Serialize(object obj);

        object Deserialize(byte[] value, Type type);

        T Deserialize<T>(byte[] value);
    }
}