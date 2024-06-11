namespace Undersoft.SDK.Service.Data.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IdentifyAttribute : Attribute
    {
        public IdentifyAttribute(Type filteredType, params string[] members) : this(members)
        {
            FilteredType = filteredType;
        }

        public IdentifyAttribute(params string[] members) : this(members, members)
        {
            FilterMembers = members;
            SortMembers = members;
        }

        public IdentifyAttribute(string[] sortMembers, params string[] filterMembers)
        {
            FilterMembers = filterMembers;
            SortMembers = sortMembers;
        }

        public string[] FilterMembers { get; set; }

        public Type FilteredType { get; set; }

        public string[] SortMembers { get; set; }
    }
}
