using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SBC.Service.Application.GUI.Compound.Presenting.Ecosystems;

public class EcosystemAplication : DataObject
{
    [Link]
    [MenuItem]
    [Extended]
    public string? Members { get; set; } = "/presenting/ecosystem/application/members";
    public Icon MembersIcon = new Icons.Regular.Size20.Compose();

    [Link]
    [MenuItem]
    [Extended]
    public string? Activities { get; set; } = "/presenting/ecosystem/application/activities";
    public Icon ActivitiesIcon = new Icons.Regular.Size20.ShiftsActivity();

    [Link]
    [MenuItem]
    [Extended]
    public string? Schedules { get; set; } = "/presenting/ecosystem/application/schedules";
    public Icon SchedulesIcon = new Icons.Regular.Size20.Calendar();

    [Link]
    [MenuItem]
    [Extended]
    public string? Resources { get; set; } = "/presenting/ecosystem/application/resources";
    public Icon ResourcesIcon = new Icons.Regular.Size20.Folder();
}

