namespace Undersoft.SDK.Service.Data.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class QueryMembersAttribute : Attribute
    {
        public QueryMembersAttribute(Type filteredType, params string[] members) : this(members)
        {
            FilteredType = filteredType;
        }

        public QueryMembersAttribute(params string[] members) : this(members, members)
        {
            FilterMembers = members;
            SortMembers = members;
        }

        public QueryMembersAttribute(string[] sortMembers, params string[] filterMembers)
        {
            FilterMembers = filterMembers;
            SortMembers = sortMembers;
        }

        public string[] FilterMembers { get; set; }

        public Type FilteredType { get; set; }

        public string[] SortMembers { get; set; }
    }
}
