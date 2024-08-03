using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.GDC.Service.Application.GUI.Compound.Presenting.Contacts;

public class PresentingContact : DataObject
{
    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Author")]
    public string Author { get; set; } = "/presenting/contact/author";
    public Icon AuthorIcon = new Icons.Regular.Size20.PenSparkle();

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Invite")]
    public string Invite { get; set; } = "/presenting/contact/invite";
    public Icon InviteIcon = new Icons.Regular.Size20.PersonAdd();

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Donate")]
    public string Donate { get; set; } = "/presenting/contact/donate";
    public Icon DonateIcon = new Icons.Regular.Size20.MoneyHand();
}

