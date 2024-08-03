using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.GDC.Service.Application.GUI.Compound.Landing;

public class LandingTopMenuItems : DataObject
{
    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Overview")]
    public string Overview { get; set; } = "/landing/overview";

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Dashboard")]
    public string Documentation { get; set; } = "/presenting/dashboard";

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Ecosystem")]
    public string Benchmarks { get; set; } = "/presenting/dashboard";

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Downloads")]
    public string Downloads { get; set; } = "/presenting/dashboard";

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Contact")]
    public string Contact { get; set; } = "/presenting/dashboard";
}

