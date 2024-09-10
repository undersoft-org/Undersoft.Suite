namespace Undersoft.SDK.Ethernet
{
    using System;

    public enum JsonModes
    {
        All,
        KeyValue,
        Array
    }

    [AttributeUsage(
        AttributeTargets.Class
            | AttributeTargets.Struct
            | AttributeTargets.Enum
            | AttributeTargets.Delegate
            | AttributeTargets.Property
            | AttributeTargets.Field,
        Inherited = false
    )]
    public sealed class JsonArrayAttribute : Attribute
    {
        public JsonArrayAttribute() { }
    }

    [AttributeUsage(
        AttributeTargets.Class
            | AttributeTargets.Struct
            | AttributeTargets.Enum
            | AttributeTargets.Delegate
            | AttributeTargets.Property
            | AttributeTargets.Field,
        Inherited = false
    )]
    public sealed class JsonIgnoreAttribute : Attribute
    {
        public JsonIgnoreAttribute() { }
    }

    [AttributeUsage(
        AttributeTargets.Class
            | AttributeTargets.Struct
            | AttributeTargets.Enum
            | AttributeTargets.Delegate
            | AttributeTargets.Property
            | AttributeTargets.Field,
        Inherited = false
    )]
    public sealed class JsonMemberAttribute : Attribute
    {
        public JsonMemberAttribute() { }

        public JsonModes SerialMode { get; set; } = JsonModes.All;
    }

    [AttributeUsage(
        AttributeTargets.Class
            | AttributeTargets.Struct
            | AttributeTargets.Enum
            | AttributeTargets.Delegate
            | AttributeTargets.Property
            | AttributeTargets.Field,
        Inherited = false
    )]
    public sealed class JsonObjectAttribute : Attribute
    {
        public JsonObjectAttribute() { }
    }
}
