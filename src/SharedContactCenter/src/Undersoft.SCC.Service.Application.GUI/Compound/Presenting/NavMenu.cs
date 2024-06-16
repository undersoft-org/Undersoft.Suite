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
    /// <value>A <see cref="Presenting.Members"/></value>
    [MenuGroup]
    [Extended]
    [IconRubric("MembersIcon")]
    [Link("/presenting/members")]
    public Members Members { get; set; } = new Members();
    /// <summary>
    /// The contacts icon.
    /// </summary>
    public Icon MembersIcon = new Icons.Regular.Size24.Apps();

    /// <summary>
    /// Gets or sets the dictionaries.
    /// </summary>
    /// <value>A <see cref="Attributes"/></value>
    [MenuGroup]
    [Extended]
    [IconRubric("AttributesIcon")]
    public Attributes Attributes { get; set; } = new Attributes();
    /// <summary>
    /// The dictionaries icon.
    /// </summary>
    public Icon AttributesIcon = new Icons.Regular.Size24.AppsListDetail();
}

