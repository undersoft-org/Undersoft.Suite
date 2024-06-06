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

namespace Undersoft.SCC.Service.Application.GUI.Compound.Presenting.Admin;

using Undersoft.SCC.Service.Application.GUI.Compound.Presenting.User;

/// <summary>
/// The admin nav menu.
/// </summary>
public class AdminNavMenu : DataObject
{
    /// <summary>
    /// Gets or sets the contacts.
    /// </summary>
    /// <value>An <see cref="AdminContacts"/></value>
    [MenuGroup]
    [Extended]
    [IconRubric("ContactsIcon")]
    [Link("/presenting/admin/contacts")]
    public AdminContacts Contacts { get; set; } = new AdminContacts();
    /// <summary>
    /// The contacts icon.
    /// </summary>
    public Icon ContactsIcon = new Icons.Regular.Size24.BookContacts();

    /// <summary>
    /// Gets or sets the dictionaries.
    /// </summary>
    /// <value>An <see cref="AdminDictionaries"/></value>
    [MenuGroup]
    [Extended]
    [IconRubric("DictionariesIcon")]
    public AdminDictionaries Dictionaries { get; set; } = new AdminDictionaries();
    /// <summary>
    /// The dictionaries icon.
    /// </summary>
    public Icon DictionariesIcon = new Icons.Regular.Size24.ArchiveSettings();

    /// <summary>
    /// Gets or sets the administration.
    /// </summary>
    /// <value>An <see cref="AdminAdministration"/></value>
    [MenuGroup]
    [Extended]
    [IconRubric("AdministrationIcon")]
    public AdminAdministration Administration { get; set; } = new AdminAdministration();
    /// <summary>
    /// The administration icon.
    /// </summary>
    public Icon AdministrationIcon = new Icons.Regular.Size24.PersonSupport();
}

