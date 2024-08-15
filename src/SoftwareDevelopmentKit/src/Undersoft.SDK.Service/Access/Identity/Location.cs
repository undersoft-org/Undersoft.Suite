using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Access.Identity
{
    public class Location : DataObject
    {
        public virtual LocationKind Kind { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
    }
}
