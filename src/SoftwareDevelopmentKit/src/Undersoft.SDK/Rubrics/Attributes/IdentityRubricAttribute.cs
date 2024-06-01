namespace Undersoft.SDK.Rubrics.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IdentityRubricAttribute : RubricAttribute
    {
        public bool IsAutoincrement = false;
        public short Order = 0;

        public IdentityRubricAttribute() { }
    }
}
