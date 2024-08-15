using System.Reflection;
using Undersoft.SDK.Updating;

namespace Undersoft.SDK.Serialization
{
    public static class BinarySerializer
    {
        private static Dictionary<string, Type> _typeMappings;
        private static Dictionary<string, IBinarySerializable> _cache;
        private static int _intHandleCounter = 0;
        private static BinarySerializable _default = null;
        public static Dictionary<string, Type> TypeMappings
        {
            get { return _typeMappings; }
        }

        static BinarySerializer()
        {
            if (_cache == null)
                _cache = new Dictionary<string, IBinarySerializable>();

            if (_typeMappings == null)
                _typeMappings = new Dictionary<string, Type>();

            ResolveAssemblies();
        }

        private static void ResolveAssemblies()
        {
            try
            {
                AssemblyName[] aa = Assembly.GetEntryAssembly().GetReferencedAssemblies();
                foreach (AssemblyName a in aa)
                {
                    AppDomain.CurrentDomain.Load(a);
                }

                Assembly[] asmbs = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly a in asmbs)
                {
                    if (IsSerializable<BinaryDocumentTraceAssembly>(a.GetCustomAttributes(false)))
                    {
                        foreach (Type t in a.GetTypes())
                        {
                            if (
                                IsSerializable<BinaryDocumentAttribute>(t.GetCustomAttributes(true))
                            )
                            {
                                Generate(t);
                            }
                        }
                    }
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        private static bool IsSerializable<ATTRIBUTE_TYPE>(object[] t)
        {
            foreach (Attribute attr in t)
            {
                if (attr is ATTRIBUTE_TYPE)
                {
                    ATTRIBUTE_TYPE a = (ATTRIBUTE_TYPE)
                        Convert.ChangeType(attr, typeof(ATTRIBUTE_TYPE));
                    return true;
                }
            }
            return false;
        }

        private static bool IsSerializable(object[] t)
        {
            bool serAttrFound = true;

            foreach (Attribute attr in t)
            {
                if (attr is SerializableAttribute)
                {
                    serAttrFound = true;
                }

                if (attr is BinaryDocumentAttribute)
                {
                    serAttrFound = false;
                }
            }

            return serAttrFound;
        }

        private static IBinarySerializable Generate(Type type)
        {
            IBinarySerializable binarySerializable = null;
            Type binarySerializableType = null;

            if (!type.IsAbstract && !IsSerializable(type.GetCustomAttributes(true)))
            {
                binarySerializableType =
                    BinarySerializableGenerator.Generate<IBinarySerializable>(
                        type
                    );
            }

            if (binarySerializableType != null)
            {
                binarySerializable = (IBinarySerializable)
                    Activator.CreateInstance(binarySerializableType);
            }
            binarySerializable.SetTypeHandle(_intHandleCounter);

            if (!_cache.ContainsKey(type.Name))
                _cache.Add(type.Name, binarySerializable);

            if (!_typeMappings.ContainsKey(type.FullName))
                _typeMappings.Add(type.FullName, type);

            return binarySerializable;
        }

        public static IBinarySerializable EnsureGet(Type type)
        {
            IBinarySerializable sr = null;

            string n = type.Name;

            if (_cache.ContainsKey(n))
                sr = _cache[n];
            else
                sr = Generate(type);

            return sr;
        }

        private static Type GetType(BinaryReader br)
        {
            Type evntType = null;
            String className = br.ReadString();
            evntType = _typeMappings[className];
            return evntType;
        }

        public static object Deserialize(Stream serializationStream)
        {
            object o = null;
            using (BinaryReader binaryReader = new BinaryReader(serializationStream))
            {
                Type t = GetType(binaryReader);
                o = EnsureGet(t).Deserialize(binaryReader);
            }
            return o;
        }

        public static object Deserialize(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
                return Deserialize(ms);
        }

        public static object Deserialize(Stream serializationStream, Type type)
        {
            object o = null;
            using (BinaryReader binaryReader = new BinaryReader(serializationStream))
            {
                Type t = GetType(binaryReader);
                o = EnsureGet(t).Deserialize(binaryReader);
                if (t != type)
                    o = o.PutTo(type);
            }
            return o;
        }

        public static T Deserialize<T>(Stream serializationStream)
        {
            return (T)Deserialize(serializationStream, typeof(T));
        }

        public static object Deserialize(byte[] buffer, Type type)
        {
            using (var ms = new MemoryStream(buffer))
                return Deserialize(ms, type);
        }

        public static T Deserialize<T>(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
                return Deserialize<T>(ms);
        }

        public static object DeserializeFromBase64String(string base64)
        {
            return Deserialize(Convert.FromBase64String(base64));
        }

        public static object DeserializeFromBase64String(string base64, Type type)
        {
            return Deserialize(Convert.FromBase64String(base64), type);
        }

        public static T DeserializeFromBase64String<T>(string base64)
        {
            return Deserialize<T>(Convert.FromBase64String(base64));
        }

        public static void Serialize<T>(Stream serializationStream, T graph)
        {
            Serialize(serializationStream, graph, typeof(T));
        }

        public static void Serialize(Stream serializationStream, object graph, Type type)
        {
            IBinarySerializable binarySerializable = EnsureGet(type);
            var binaryWriter = new BinaryWriter(serializationStream);
            binaryWriter.Write(type.FullName);
            binarySerializable.Serialize(binaryWriter, graph);
        }

        public static void Serialize(Stream serializationStream, object graph)
        {
            Serialize(serializationStream, graph, graph.GetType());
        }

        public static byte[] Serialize(object graph)
        {
            using (var ms = new MemoryStream())
            {
                Serialize(ms, graph);
                return ms.ToArray();
            }
        }

        public static byte[] Serialize<T>(T graph)
        {
            using (var ms = new MemoryStream())
            {
                Serialize(ms, graph);
                return ms.ToArray();
            }
        }

        public static byte[] Serialize(object graph, Type type)
        {
            if (graph.GetType() != type)
                graph = graph.PutTo(type);
            using (var ms = new MemoryStream())
            {
                Serialize(ms, graph, type);
                return ms.ToArray();
            }
        }

        public static string SerializeToBase64String(object graph)
        {
            return Convert.ToBase64String(Serialize(graph));
        }

        public static string SerializeToBase64String(object graph, Type type)
        {
            return Convert.ToBase64String(Serialize(graph, type));
        }
    }
}
