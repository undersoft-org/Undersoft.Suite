namespace Undersoft.SDK.Service.Data.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ViewClassAttribute : Attribute
    {
        public ViewClassAttribute() { }

        public ViewClassAttribute(string @class) { Class = @class; }

        public string Class { get; set; }
    }
}
