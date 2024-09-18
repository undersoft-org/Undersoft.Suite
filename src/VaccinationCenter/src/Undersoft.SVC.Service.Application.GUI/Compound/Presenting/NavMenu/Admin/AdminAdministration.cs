using Microsoft.FluentUI.AspNetCore.Components;

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SVC.Service.Application.GUI.Compound.Presenting.NavMenu.Admin;

public class AdminAdministration : DataObject
{
    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Accounts")]
    [IconRubric("AccountsIcon")]
    public string Accounts { get; set; } = "/presenting/admin/administration/accounts";

    public Icon AccountsIcon = new Icons.Regular.Size20.PersonAccounts();

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Events")]
    [IconRubric("EventsIcon")]
    public string Events { get; set; } = "/presenting/admin/administration/events";

    public Icon EventsIcon = new Icons.Regular.Size20.TableLightning();
}

