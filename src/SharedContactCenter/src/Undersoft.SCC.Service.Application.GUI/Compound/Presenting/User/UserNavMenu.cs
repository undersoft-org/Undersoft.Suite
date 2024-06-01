using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Presenting.User;

public class UserNavMenu : DataObject
{
    [MenuGroup]
    [Extended]
    [IconRubric("ContactsIcon")]
    [Link("/presenting/user/contacts")]
    public UserContacts Contacts { get; set; } = new UserContacts();
    public Icon ContactsIcon = new Icons.Regular.Size24.BookContacts();

    [MenuGroup]
    [Extended]
    [IconRubric("DictionariesIcon")]
    public UserDictionaries Dictionaries { get; set; } = new UserDictionaries();
    public Icon DictionariesIcon = new Icons.Regular.Size24.ArchiveSettings();
}

