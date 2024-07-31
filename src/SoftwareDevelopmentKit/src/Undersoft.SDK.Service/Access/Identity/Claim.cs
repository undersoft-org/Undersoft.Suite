using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SDK.Service.Access.Identity
{
    public class Claim : InnerProxy, IClaim
    {
        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Type")]
        public virtual string ClaimType { get; set; }

        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Value")]
        public virtual string ClaimValue { get; set; }
    }
}