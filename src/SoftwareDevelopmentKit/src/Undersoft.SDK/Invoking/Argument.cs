namespace Undersoft.SDK.Invoking
{
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Undersoft.SDK.Uniques;
    using Undersoft.SDK.Utilities;

    [DataContract]
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class Argument : Identifiable, IArgument
    {
        [JsonIgnore]
        [IgnoreDataMember]
        protected Type _type;

        public Argument() { }

        public Argument(IArgument value, string method, string target = null)
        {
            Set(value, method, target);
        }

        public Argument(object value, string method, string target = null)
        {
            Set(value.GetType().Name, value, method, target);
        }

        public Argument(string name, object value, string method, string target = null)
        {
            Set(name, value, method, target);
        }

        public Argument(string name, Type type, string method, string target = null)
        {
            Set(name, type.Default(), method, target);
        }

        public Argument(string name, string argTypeName, string method, string target = null)
        {
            Set(name, AssemblyUtilities.FindTypeByFullName(argTypeName), method, target);
        }

        public Argument(ParameterInfo info, string method, string target = null)
        {
            Set(info, method, target);
        }

        public string TargetName { get; set; }

        public string MethodName { get; set; }

        public string Name { get; set; }

        public byte[] Data { get; set; }

        public long DataKey { get; set; }

        public bool IsValid { get; set; } = true;

        public int Position { get; set; } = -1;

        public string ArgumentTypeName { get; set; }

        public Type ResolveType()
        {
            return _type ??= AssemblyUtilities.FindTypeByFullName(ArgumentTypeName);
        }

        public void Serialize(object value)
        {
            if (value != null)
            {
                var vt = value.GetType();
                if (vt.IsAssignableTo(typeof(IIdentifiable)))
                    DataKey = ((IIdentifiable)value).Id;
                Data = value.ToJsonBytes();
                ArgumentTypeName = vt.FullName;
            }
        }

        public object Deserialize()
        {
            if (Data != null && ArgumentTypeName != null)
            {
                var t = ResolveType();
                if (t != null)
                    return Data.FromJson(t);
            }
            return null;
        }

        public T Deserialize<T>()
        {
            if (Data != null)
            {
                return Data.FromJson<T>();
            }
            return default(T);
        }

        public void Set(ParameterInfo item, string method, string target = null)
        {
            Name = item.Name;
            _type = item.ParameterType;
            Position = item.Position;
            ArgumentTypeName = _type.FullName;
            MethodName = method;
            TargetName = target;
            Id = $"{ArgumentTypeName}_{Name}".UniqueKey();
            TypeId = ArgumentTypeName.UniqueKey();
        }

        public void Set(IArgument item, string method, string target = null)
        {
            Name = item.Name;
            Data = item.Data;
            Position = item.Position;
            ArgumentTypeName = item.ArgumentTypeName;
            MethodName = method;
            TargetName = target;
            Id = $"{ArgumentTypeName}_{Name}".UniqueKey();
            TypeId = ArgumentTypeName.UniqueKey();
        }

        public virtual void Set(
            string name,
            object value,
            string method,
            string target = null,
            int position = 0
        )
        {
            Name = name;
            _type = value.GetType();
            ArgumentTypeName = _type.FullName;
            Serialize(value);
            Position = position;
            MethodName = method;
            TargetName = target;
            Id = $"{ArgumentTypeName}_{Name}".UniqueKey();
            TypeId = ArgumentTypeName.UniqueKey();
        }

        public void Set(
            string name,
            Type type,
            string method,
            string target = null,
            int position = 0
        )
        {
            Name = name;
            _type = type;
            ArgumentTypeName = _type.FullName;
            Position = position;
            MethodName = method;
            TargetName = target;
            Id = $"{ArgumentTypeName}_{Name}".UniqueKey();
            TypeId = ArgumentTypeName.UniqueKey();
        }
    }
}
