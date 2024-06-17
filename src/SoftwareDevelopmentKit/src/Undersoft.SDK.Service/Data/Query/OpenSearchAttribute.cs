namespace Undersoft.SDK.Service.Data.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OpenSearchAttribute : Attribute
    {
        public OpenSearchAttribute(params string[] members)
        {
            SearchMembers = members;
        }

        public string[] SearchMembers { get; set; }
    }
}
