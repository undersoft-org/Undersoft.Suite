namespace Undersoft.SDK.Service.Application.GUI.View.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ClassAttribute : Attribute
    {
        public ClassAttribute() { }

        public ClassAttribute(string @class) { Class = @class; }

        public string Class { get; set; }
    }
}
