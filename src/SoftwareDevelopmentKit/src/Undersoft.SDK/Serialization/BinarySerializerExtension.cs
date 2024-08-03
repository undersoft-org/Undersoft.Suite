using Undersoft.SDK.Extracting;
using Undersoft.SDK.Serialization;
using Undersoft.SDK.Updating;

namespace System.Text.Json
{
    public static class BinarySerializerExtension
    {
        public static byte[] ToBinaryBytes(this object obj)
        {
            return BinarySerializer.Serialize(obj);
        }

        public static byte[] ToBinaryBytes(this object obj, Type type)
        {
            return BinarySerializer.Serialize(obj, type);
        }

        public static byte[] ToBinaryBytes<T>(this T obj)
        {
            return BinarySerializer.Serialize(obj);
        }

        public static string ToBinaryString(this object obj)
        {
            return BinarySerializer.SerializeToBase64String(obj);
        }

        public static string ToBinaryString<T>(this T obj)
        {
            return BinarySerializer.SerializeToBase64String(obj);
        }

        public static void ToBinaryStream(this object obj, Stream stream)
        {
            BinarySerializer.Serialize(stream, obj);
        }

        public static void ToBinaryStream<T>(this T obj, Stream stream)
        {
            BinarySerializer.Serialize(stream, obj);
        }

        public static object ToBinaryBuffer(this object obj, ref byte[] buffer)
        {
            byte[] src = BinarySerializer.Serialize(obj);
            src.CopyBlock(buffer, (ulong)src.Length);
            return obj;
        }

        public static T ToBinaryBuffer<T>(this T obj, ref byte[] buffer)
        {
            byte[] src = BinarySerializer.Serialize(obj);
            src.CopyBlock(buffer, (ulong)src.Length);
            return obj;
        }

        public static object FromBinary(this byte[] bytes, Type type)
        {
            return BinarySerializer.Deserialize(bytes, type);
        }

        public static T FromBinary<T>(this byte[] bytes)
        {
            return BinarySerializer.Deserialize<T>(bytes);
        }

        public static object FromBinary(this Type type, byte[] bytes)
        {
            return BinarySerializer.Deserialize(bytes, type);
        }

        public static object FromBinary(this string str, Type type)
        {
            return BinarySerializer.DeserializeFromBase64String(str, type);
        }

        public static T FromBinary<T>(this string str)
        {
            return BinarySerializer.DeserializeFromBase64String<T>(str);
        }

        public static object FromBinary(this Type type, string str)
        {
            return BinarySerializer.DeserializeFromBase64String(str, type);
        }

        public static object FromBinary(this Stream str, Type type)
        {
            return BinarySerializer.Deserialize(str, type);
        }

        public static T FromBinary<T>(this Stream str)
        {
            return BinarySerializer.Deserialize<T>(str);
        }

        public static object FromBinary(this Type type, Stream str)
        {
            return BinarySerializer.Deserialize(str, type);
        }

        public static E PatchFromBinary<T, E>(this E obj, string str) where T : class where E : class
        {
            return str.FromBinary<T>().PatchTo<T, E>(obj);
        }

        public static E PutFromBinary<T, E>(this E obj, string str) where T : class where E : class
        {
            return str.FromBinary<T>().PutTo<T, E>(obj);
        }

        public static E PatchFromBinary<T, E>(this E obj, byte[] bytes) where T : class where E : class
        {
            return bytes.FromBinary<T>().PatchTo<T, E>(obj);
        }

        public static E PutFromBinary<T, E>(this E obj, byte[] bytes) where T : class where E : class
        {
            return bytes.FromBinary<T>().PutTo<T, E>(obj);
        }

        public static E PatchFromBinary<T, E>(this E obj, Stream str) where T : class where E : class
        {
            return str.FromBinary<T>().PatchTo<T, E>(obj);
        }

        public static E PutFromBinary<T, E>(this E obj, Stream str) where T : class where E : class
        {
            return str.FromBinary<T>().PutTo<T, E>(obj);
        }
    }
}
