using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Presenting.Admin;

public class AdminAdministration : DataObject
{
    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Accounts")]
    public string Accounts { get; set; } = "/presenting/admin/administration/accounts";
    public Icon AccountsIcon = new Icons.Regular.Size20.PersonAccounts();

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Events")]
    public string Events { get; set; } = "/presenting/admin/administration/events";
    public Icon EventsIcon = new Icons.Regular.Size20.TableLightning();
}

