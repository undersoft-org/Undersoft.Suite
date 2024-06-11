namespace Undersoft.SDK.Service.Data.Query
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class OpenQueryAttribute : Attribute
    {
        public OpenQueryAttribute(Type filteredType, params string[] members) : this(members)
        {
            FilteredType = filteredType;
        }

        public OpenQueryAttribute(params string[] members) : this(members, members)
        {
            FilterMembers = members;
            SortMembers = members;
        }

        public OpenQueryAttribute(string[] sortMembers, params string[] filterMembers)
        {
            FilterMembers = filterMembers;
            SortMembers = sortMembers;
        }

        public string[] FilterMembers { get; set; }

        public Type FilteredType { get; set; }

        public string[] SortMembers { get; set; }

        public string[] AggregateMembers { get; set; }

        public string[] ExpandMembers { get; set; }
    }
}
