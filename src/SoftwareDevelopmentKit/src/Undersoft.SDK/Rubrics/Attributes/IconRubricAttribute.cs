namespace Undersoft.SDK.Rubrics.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class IconRubricAttribute : RubricAttribute
    {
        public string IconMember;
        public IconSlot IconSlot;

        public IconRubricAttribute(string iconMember, IconSlot iconSlot)
        {
            IconMember = iconMember;
            IconSlot = iconSlot;
        }

        public IconRubricAttribute(string iconMember)
        {
            IconMember = iconMember;
        }
    }

    public enum IconSlot
    {
        Start,
        End
    }
}
