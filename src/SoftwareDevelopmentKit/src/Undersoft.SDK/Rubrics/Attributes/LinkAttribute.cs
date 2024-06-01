namespace Undersoft.SDK.Rubrics.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class LinkAttribute : RubricAttribute
    {
        public string Value;

        public bool PrefixedLink = true;

        public LinkAttribute() { }

        public LinkAttribute(string link)
        {
            Value = link;
        }

        public LinkAttribute(string link, bool prefixedLink)
        {
            Value = link;
            PrefixedLink = prefixedLink;
        }
    }
}
