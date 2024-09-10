using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SSC.Service.Application.GUI.Compound.Presenting.Ecosystems;

public class EcosystemService : DataObject
{
    [Link]
    [MenuItem]
    [Extended]
    public string? Members { get; set; } = "/presenting/ecosystem/service/members";
    public Icon MembersIcon = new Icons.Regular.Size20.Compose();

    [Link]
    [MenuItem]
    [Extended]
    public string? Activities { get; set; } = "/presenting/ecosystem/service/activities";
    public Icon ActivitiesIcon = new Icons.Regular.Size20.ShiftsActivity();

    [Link]
    [MenuItem]
    [Extended]
    public string? Schedules { get; set; } = "/presenting/ecosystem/service/schedules";
    public Icon SchedulesIcon = new Icons.Regular.Size20.Calendar();

    [Link]
    [MenuItem]
    [Extended]
    public string? Resources { get; set; } = "/presenting/ecosystem/service/resources";
    public Icon ResourcesIcon = new Icons.Regular.Size20.Folder();
}

