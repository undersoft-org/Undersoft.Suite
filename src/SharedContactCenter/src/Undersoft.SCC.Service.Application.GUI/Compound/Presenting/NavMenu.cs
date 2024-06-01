using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Presenting;

public class NavMenu : DataObject
{
    [MenuGroup]
    [Extended]
    [IconRubric("ContactsIcon")]
    [Link("/presenting/contacts")]
    public Contacts Contacts { get; set; } = new Contacts();
    public Icon ContactsIcon = new Icons.Regular.Size24.BookContacts();

    [MenuGroup]
    [Extended]
    [IconRubric("DictionariesIcon")]
    public Dictionaries Dictionaries { get; set; } = new Dictionaries();
    public Icon DictionariesIcon = new Icons.Regular.Size24.ArchiveSettings();
}

