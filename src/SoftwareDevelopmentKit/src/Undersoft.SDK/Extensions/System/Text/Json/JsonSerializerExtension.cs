using System.Reflection;
using System.Text.Json.Serialization;
using Undersoft.SDK.Extracting;
using Undersoft.SDK.Updating;

namespace System.Text.Json
{
    public static class JsonSerializerExtension
    {
        private static JsonSerializerOptions _jsonOptions;

        static JsonSerializerExtension()
        {
            _jsonOptions = new JsonSerializerOptions();
            _jsonOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            _jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            _jsonOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
            _jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            _jsonOptions.IgnoreReadOnlyProperties = true;
            _jsonOptions.IgnoreReadOnlyProperties = true;
            _jsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, true));
            //_jsonOptions.Converters.Add(new BinaryJsonConverter());


#if NET6_0
            var fld = (
                typeof(JsonSerializerOptions).GetField(
                    "s_defaultOptions",
                    System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic
                )
            );

            var opt = (JsonSerializerOptions)fld.GetValue(_jsonOptions);
            if (opt == null)
                fld.SetValue(_jsonOptions, _jsonOptions);
            else
                _jsonOptions.PatchTo(opt);
#endif
#if NET8_0
            var flds = typeof(JsonSerializerOptions).GetRuntimeFields();
            flds.Single(f => f.Name == "_defaultIgnoreCondition")
                .SetValue(JsonSerializerOptions.Default, JsonIgnoreCondition.WhenWritingNull);
            flds.Single(f => f.Name == "_referenceHandler")
                .SetValue(JsonSerializerOptions.Default, ReferenceHandler.IgnoreCycles);
#endif
        }

        public static byte[] ToJsonBytes(this object obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj, obj.GetType(), _jsonOptions);
        }

        public static byte[] ToJsonBytes(this object obj, Type type)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj, type, _jsonOptions);
        }

        public static JsonDocument ToJsonDocument(this object obj, Type type)
        {
            return JsonSerializer.SerializeToDocument(obj, type, _jsonOptions);
        }

        public static JsonDocument ToJsonDocument<T>(this T obj)
        {
            return JsonSerializer.SerializeToDocument<T>(obj, _jsonOptions);
        }

        public static JsonElement ToJsonElement(this object obj, Type type)
        {
            return JsonSerializer.SerializeToElement(obj, type, _jsonOptions);
        }

        public static JsonElement ToJsonElement<T>(this T obj)
        {
            return JsonSerializer.SerializeToElement<T>(obj, _jsonOptions);
        }

        public static object FromJsonDocument(this Type type, JsonDocument doc)
        {
            return JsonSerializer.Deserialize(doc, type, _jsonOptions);
        }

        public static T FromJsonDocument<T>(this JsonDocument doc)
        {
            return JsonSerializer.Deserialize<T>(doc, _jsonOptions);
        }

        public static T FromJsonDocument<T>(this T obj, JsonDocument doc) where T : class
        {
            return obj.PatchFrom(JsonSerializer.Deserialize<T>(doc, _jsonOptions));
        }

        public static object FromJsonElement(this Type type, JsonElement doc)
        {
            return JsonSerializer.Deserialize(doc, type, _jsonOptions);
        }

        public static T FromJsonElement<T>(this JsonElement doc)
        {
            return JsonSerializer.Deserialize<T>(doc, _jsonOptions);
        }

        public static T FromJsonElement<T>(this T obj, JsonElement doc) where T : class
        {
            return obj.PatchFrom(JsonSerializer.Deserialize<T>(doc, _jsonOptions));
        }

        public static byte[] ToJsonBytes<T>(this T obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj, _jsonOptions);
        }

        public static string ToJsonString(this object obj)
        {
            return JsonSerializer.Serialize(obj, obj.GetType(), _jsonOptions);
        }

        public static string ToJsonString<T>(this T obj)
        {
            return JsonSerializer.Serialize(obj, _jsonOptions);
        }

        public static Task ToJsonStream(this object obj, Stream stream)
        {
            return JsonSerializer.SerializeAsync(stream, obj, _jsonOptions);
        }

        public static Task ToJsonStream<T>(this T obj, Stream stream)
        {
            return JsonSerializer.SerializeAsync(stream, obj, _jsonOptions);
        }

        public static object ToJsonBuffer(this object obj, ref byte[] buffer)
        {
            byte[] src = JsonSerializer.SerializeToUtf8Bytes(obj, obj.GetType(), _jsonOptions);
            src.CopyBlock(buffer, (ulong)src.Length);
            return obj;
        }

        public static T ToJsonBuffer<T>(this T obj, ref byte[] buffer)
        {
            byte[] src = JsonSerializer.SerializeToUtf8Bytes(obj, obj.GetType(), _jsonOptions);
            src.CopyBlock(buffer, (ulong)src.Length);
            return obj;
        }

        public static object FromJson(this byte[] bytes, Type type)
        {
            return JsonSerializer.Deserialize(bytes, type, _jsonOptions);
        }

        public static T FromJson<T>(this byte[] bytes)
        {
            return JsonSerializer.Deserialize<T>(bytes, _jsonOptions);
        }

        public static object FromJson(this Type type, byte[] bytes)
        {
            return JsonSerializer.Deserialize(bytes, type, _jsonOptions);
        }

        public static object FromJson(this string str, Type type)
        {
            return JsonSerializer.Deserialize(str, type, _jsonOptions);
        }

        public static T FromJson<T>(this string str)
        {
            return JsonSerializer.Deserialize<T>(str, _jsonOptions);
        }

        public static object FromJson(this Type type, string str)
        {
            return JsonSerializer.Deserialize(str, type, _jsonOptions);
        }

        public static object FromJson(this Stream str, Type type)
        {
            return JsonSerializer.Deserialize(str, type, _jsonOptions);
        }

        public static T FromJson<T>(this Stream str)
        {
            return JsonSerializer.Deserialize<T>(str, _jsonOptions);
        }

        public static object FromJson(this Type type, Stream str)
        {
            return JsonSerializer.Deserialize(str, type, _jsonOptions);
        }

        public static E PatchFromJson<T, E>(this E obj, string str) where T : class where E : class
        {
            return str.FromJson<T>().PatchTo<T, E>(obj);
        }

        public static E PutFromJson<T, E>(this E obj, string str) where T : class where E : class
        {
            return str.FromJson<T>().PutTo<T, E>(obj);
        }

        public static E PatchFromJson<T, E>(this E obj, byte[] bytes) where T : class where E : class
        {
            return bytes.FromJson<T>().PatchTo<T, E>(obj);
        }

        public static E PutFromJson<T, E>(this E obj, byte[] bytes) where T : class where E : class
        {
            return bytes.FromJson<T>().PutTo<T, E>(obj);
        }

        public static E PatchFromJson<T, E>(this E obj, Stream str) where T : class where E : class
        {
            return str.FromJson<T>().PatchTo<T, E>(obj);
        }

        public static E PutFromJson<T, E>(this E obj, Stream str) where T : class where E : class
        {
            return str.FromJson<T>().PutTo<T, E>(obj);
        }
    }
}
