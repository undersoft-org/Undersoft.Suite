using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.GDC.Service.Application.GUI.Compound.Presenting.Ecosystems;

public class EcosystemCenter : DataObject
{
    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Services")]
    public string? Services { get; set; } = "/presenting/ecosystem/center/services";
    public Icon ServicesIcon = new Icons.Regular.Size20.Apps();

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Applications")]
    public string? Applications { get; set; } = "/presenting/ecosystem/center/applications";
    public Icon ApplicationsIcon = new Icons.Regular.Size20.Apps();

    [Link]
    [MenuItem]
    [Extended]
    public string? Accounts { get; set; } = "/presenting/ecosystem/center/accounts";
    public Icon AccountsIcon = new Icons.Regular.Size20.PeopleSettings();
}

