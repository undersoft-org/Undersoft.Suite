namespace Undersoft.SDK.Service.Data.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ViewQueryAttribute : Attribute
    {
        public ViewQueryAttribute(Type filteredType, params string[] members) : this(members)
        {
            FilteredType = filteredType;
        }

        public ViewQueryAttribute(params string[] members)
        {
            FilterMembers = members;
            SortMembers = members;
        }

        public string[] FilterMembers { get; set; }

        public Type FilteredType { get; set; } = typeof(string);

        public string[] SortMembers { get; set; }
    }
}
