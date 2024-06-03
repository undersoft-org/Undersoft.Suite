namespace Undersoft.SDK.Service.Data.Model.Attributes
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class ViewSizeAttribute : Attribute
    {
        public ViewSizeAttribute(string width = null, string height = null, string z = null) { Width = width; Height = height; Z = z; }

        public string Width { get; set; }

        public string Height { get; set; }

        public string Z { get; set; }
    }
}
