using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting.User;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Presenting.Admin;

public class AdminNavMenu : DataObject
{
    [MenuGroup]
    [Extended]
    [IconRubric("ContactsIcon")]
    [Link("/presenting/admin/contacts")]
    public AdminContacts Contacts { get; set; } = new AdminContacts();
    public Icon ContactsIcon = new Icons.Regular.Size24.BookContacts();

    [MenuGroup]
    [Extended]
    [IconRubric("DictionariesIcon")]
    public AdminDictionaries Dictionaries { get; set; } = new AdminDictionaries();
    public Icon DictionariesIcon = new Icons.Regular.Size24.ArchiveSettings();

    [MenuGroup]
    [Extended]
    [IconRubric("AdministrationIcon")]
    public AdminAdministration Administration { get; set; } = new AdminAdministration();
    public Icon AdministrationIcon = new Icons.Regular.Size24.ArchiveSettings();
}

