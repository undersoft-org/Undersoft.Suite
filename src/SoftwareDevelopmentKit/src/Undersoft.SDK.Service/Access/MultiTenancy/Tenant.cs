using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Access.MultiTenancy;

public class Tenant : DataObject, ITenant
{
    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Name")]
    public string TenantName { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Url")]
    public string TenantUrl { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Path")]
    public string TenantPath { get; set; }
}
