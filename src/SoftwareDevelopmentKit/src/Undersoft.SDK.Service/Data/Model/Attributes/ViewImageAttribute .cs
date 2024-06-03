namespace Undersoft.SDK.Service.Data.Model.Attributes
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ViewImageAttribute : ViewSizeAttribute
    {
        public ViewImageAttribute(ViewImageMode mode = ViewImageMode.Regular, string width = null, string height = null) : base(width, height) { Mode = mode; }

        public ViewImageMode Mode { get; set; }
    }

    public enum ViewImageMode
    {
        None,
        Regular,
        Persona
    }

}
