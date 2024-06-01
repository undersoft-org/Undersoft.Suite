namespace Undersoft.SDK.Rubrics.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class DisplayRubricAttribute : RubricAttribute
    {
        public string Name;

        public DisplayRubricAttribute(string name)
        {
            Name = name;
        }
    }
}
