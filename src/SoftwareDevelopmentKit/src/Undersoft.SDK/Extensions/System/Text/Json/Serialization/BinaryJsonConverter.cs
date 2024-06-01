using System.Text.Json;

namespace System.Text.Json.Serialization
{
    public sealed class BinaryJsonConverter : JsonConverter<byte[]>
    {
        public BinaryJsonConverter() { }

        public sealed override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetBytesFromBase64(out byte[] bytes))
                return bytes;
            return null;
        }

        public sealed override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
        {
            if (value != null)
                writer.WriteBase64StringValue(value);
        }
    }
}