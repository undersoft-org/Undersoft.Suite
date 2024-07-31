using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SDK.Service.Access.Identity
{
    public class Role : InnerProxy, IRole, IContract
    {
        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Name")]
        public virtual string Name { get; set; }

        public virtual string NormalizedName { get; set; }

        public Listing<Claim> Claims { get; set; }
    }
}