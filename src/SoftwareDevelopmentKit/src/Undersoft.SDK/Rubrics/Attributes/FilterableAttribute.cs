namespace Undersoft.SDK.Rubrics.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FilterableAttribute : Attribute
    {
        public FilterableAttribute() { }
    }
}
