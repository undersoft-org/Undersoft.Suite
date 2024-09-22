namespace Undersoft.SDK.Rubrics.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class DisplayRubricAttribute : RubricAttribute
    {
        public string Name;
        public string Description;
        public string GroupName;
        public int Order;
        public string[] FilterMembers;
        public string[] SortMembers;
        public string[] FilteredType;
        public string[] AggregateMembers;
        public bool Sortable => SortMembers.Any();
        public bool Filterable => FilterMembers.Any();

        public DisplayRubricAttribute(string name)
        {
            Name = name;
        }

        public string[] QueryMembers
        {
            set
            {
                FilterMembers = value;
                SortMembers = value;
            }
        }
    }
}
