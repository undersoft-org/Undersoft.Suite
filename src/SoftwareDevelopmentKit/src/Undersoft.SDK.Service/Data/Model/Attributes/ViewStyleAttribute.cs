namespace Undersoft.SDK.Service.Data.Model.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ViewStyleAttribute : Attribute
    {
        public ViewStyleAttribute(string style) { Style = style; }

        public string Style { get; set; }
    }
}
