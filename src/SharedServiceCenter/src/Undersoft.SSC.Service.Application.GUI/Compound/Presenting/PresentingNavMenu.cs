using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SSC.Service.Application.GUI.Compound.Presenting.Contacts;
using Undersoft.SSC.Service.Application.GUI.Compound.Presenting.Dashboard;
using Undersoft.SSC.Service.Application.GUI.Compound.Presenting.Downloads;
using Undersoft.SSC.Service.Application.GUI.Compound.Presenting.Ecosystems;

namespace Undersoft.SSC.Service.Application.GUI.Compound.Presenting;

public class PresentingNavMenu : DataObject
{
    [MenuGroup]
    [Extended]
    [IconRubric("DashboardIcon")]
    [Link("/presenting/dashboard")]
    public PresentingDashboard Dashboard { get; set; } = new PresentingDashboard();
    public Icon DashboardIcon = new Icons.Regular.Size24.ChartMultiple();

    [MenuGroup]
    [Extended]
    [IconRubric("EcosystemIcon")]
    public PresentingEcosystem Ecosystem { get; set; } = new PresentingEcosystem();
    public Icon EcosystemIcon = new Icons.Regular.Size24.TetrisApp();

    [MenuGroup]
    [Extended]
    [IconRubric("DownloadsIcon")]
    public PresentingDownloads Downloads { get; set; } = new PresentingDownloads();
    public Icon DownloadsIcon = new Icons.Regular.Size24.CalendarArrowDown();

    [MenuGroup]
    [Extended]
    [IconRubric("ContactIcon")]
    public PresentingContact Contact { get; set; } = new PresentingContact();
    public Icon ContactIcon = new Icons.Regular.Size24.BookContacts();
}

