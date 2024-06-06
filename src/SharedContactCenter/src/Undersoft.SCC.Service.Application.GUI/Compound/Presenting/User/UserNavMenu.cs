using Microsoft.FluentUI.AspNetCore.Components;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Application.GUI
// ********************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Presenting.User;

/// <summary>
/// The user nav menu.
/// </summary>
public class UserNavMenu : DataObject
{
    /// <summary>
    /// Gets or sets the contacts.
    /// </summary>
    /// <value>An <see cref="UserContacts"/></value>
    [MenuGroup]
    [Extended]
    [IconRubric("ContactsIcon")]
    [Link("/presenting/user/contacts")]
    public UserContacts Contacts { get; set; } = new UserContacts();
    /// <summary>
    /// The contacts icon.
    /// </summary>
    public Icon ContactsIcon = new Icons.Regular.Size24.BookContacts();

    /// <summary>
    /// Gets or sets the dictionaries.
    /// </summary>
    /// <value>An <see cref="UserDictionaries"/></value>
    [MenuGroup]
    [Extended]
    [IconRubric("DictionariesIcon")]
    public UserDictionaries Dictionaries { get; set; } = new UserDictionaries();
    /// <summary>
    /// The dictionaries icon.
    /// </summary>
    public Icon DictionariesIcon = new Icons.Regular.Size24.ArchiveSettings();
}

