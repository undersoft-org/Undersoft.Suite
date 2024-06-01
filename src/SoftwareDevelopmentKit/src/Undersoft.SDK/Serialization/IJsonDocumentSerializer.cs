using System.Text.Json;

namespace Undersoft.SDK.Serialization
{
    public interface IJsonDocumentSerializer
    {
        JsonDocument Document { get; set; }
        Type Type { get; }
        string TypeName { get; set; }

        object Deserialize(byte[] data);
        T Deserialize<T>(byte[] data);
        object FromDocument(JsonDocument doc);
        T FromDocument<T>(JsonDocument doc);
        object FromElement(JsonElement ele, Type type);
        T FromElement<T>(JsonElement ele);
        object GetObject();
        T GetObject<T>();
        object GetProperty(Type type, Func<JsonDocument, JsonElement> select);
        T GetProperty<T>(Func<JsonDocument, JsonElement> select);
        byte[] Serialize(object detail);
        byte[] Serialize<T>(T detail);
        void SetDocument(object structure);
        void SetDocument<T>(T structure);
        void SetElement<T, E>(E element, Func<T, E> select);
        void SetElement<T>(object element, Func<T, object> select);
        JsonDocument ToDocument(object detail);
        JsonDocument ToDocument<T>(T detail);
        JsonElement ToElement(object detail);
        JsonElement ToElement<T>(T detail);
    }
}