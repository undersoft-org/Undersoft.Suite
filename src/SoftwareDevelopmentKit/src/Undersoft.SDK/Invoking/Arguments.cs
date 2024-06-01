namespace Undersoft.SDK.Invoking
{
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;
    using Undersoft.SDK.Series;

    [DataContract]
    [Serializable]
    public class Arguments : Listing<Argument>
    {
        public Arguments() : base() { }

        public Arguments(Type targetType) : base()
        {
            TargetType = targetType;
        }

        public Arguments(string targetName) : base()
        {
            TargetType = Assemblies.FindTypeByFullName(targetName);
        }

        public Arguments(string methodName, object argValue, string targetName = null, Type targetType = null)
           : this(methodName, new Argument(argValue, methodName, targetName), targetName, targetType) { }
        
        public Arguments(string methodName, string argName, object argValue, string targetName = null, Type targetType = null)
            : this(methodName, new Argument(argName, argValue, methodName, targetName), targetName, targetType) { }
        
        public Arguments(string methodName, Argument argument, string targetName = null, Type targetType = null)
            : this(targetType)
        {
            argument.MethodName = methodName;
            argument.TargetName = targetName;   
            this.Add(argument);
        }

        public Arguments(string methodName, IEnumerable<object> values, string targetName = null, Type targetType = null)
           : this(targetType)
        {
            values.ForEach(v => this.Add(new Argument(v, methodName, targetName)));
        }

        public Arguments(string methodName, IEnumerable<Argument> arguments, string targetName = null, Type targetType = null)
            : this(targetType)
        {
            arguments.ForEach(a => a.MethodName = methodName).Commit();
            arguments.ForEach(a => a.TargetName = targetName).Commit();
            this.Add(arguments);
        }

        public Arguments(string methodName, ParameterInfo[] parameters, string targetName = null, Type targetType = null)
            : this(targetType)
        {
            this.Add(parameters.DoEach(p => new Argument(p, methodName, targetName)));
        }

        public Arguments(            
            string methodName,
            Dictionary<string, object> dictionary,
            string targetName
        ) : this(methodName, targetName)
        {
            this.Add(dictionary.ForEach(p => new Argument(p.Key, p.Value, methodName, targetName)));
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public object[] ValueArray => this.ForEach(i => i.Deserialize()).ToArray();

        private Type[] _typeArray;
        [JsonIgnore]
        [IgnoreDataMember]
        public Type[] TypeArray => _typeArray ??= this.ForEach(a => a.ResolveType()).ToArray();

        public Type[] ResolveArgumentTypes()
        {
            _typeArray = null;
            return TypeArray;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public Type TargetType { get; set; }

        public string TargetName => this.FirstOrDefault()?.TargetName;

        public string MethodName => this.FirstOrDefault()?.MethodName;

        public bool IsValid => !this.Any(p => !p.IsValid);

        public Argument New(string name, object value, string method, string target)
        {
            return this.Put(new Argument(name, value, method, target)).Value;
        }

        public ISeriesItem<Argument> New(object value, string method, string target = null)
        {
            return this.Put(new Argument(value.GetType().Name, value, method, target));
        }
    }
}
