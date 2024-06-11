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

namespace Undersoft.SCC.Service.Application.GUI.Compound.Presenting;

/// <summary>
/// The nav menu.
/// </summary>
public class NavMenu : DataObject
{
    /// <summary>
    /// Gets or sets the contacts.
    /// </summary>
    /// <value>A <see cref="Contacts"/></value>
    [MenuGroup]
    [Extended]
    [IconRubric("ContactsIcon")]
    [Link("/presenting/contacts")]
    public Contacts Contacts { get; set; } = new Contacts();
    /// <summary>
    /// The contacts icon.
    /// </summary>
    public Icon ContactsIcon = new Icons.Regular.Size24.BookContacts();

    /// <summary>
    /// Gets or sets the dictionaries.
    /// </summary>
    /// <value>A <see cref="Dictionaries"/></value>
    [MenuGroup]
    [Extended]
    [IconRubric("DictionariesIcon")]
    public Dictionaries Dictionaries { get; set; } = new Dictionaries();
    /// <summary>
    /// The dictionaries icon.
    /// </summary>
    public Icon DictionariesIcon = new Icons.Regular.Size24.BookOpen();
}

