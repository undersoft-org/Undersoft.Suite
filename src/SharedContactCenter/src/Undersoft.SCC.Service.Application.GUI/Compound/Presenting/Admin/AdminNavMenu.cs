using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting.User;


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
/// <summary>
/// The admin nav menu.
/// </summary>
public class AdminNavMenu : DataObject
{
    /// <summary>
    /// Gets or sets the contacts.
    /// </summary>
    /// <value>A <see cref="Presenting.Members"/></value>
    [MenuGroup]
    [Extended]
    [IconRubric("MembersIcon")]
    [Link("/presenting/admin/members")]
    public AdminMembers Members { get; set; } = new AdminMembers();
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
    public AdminAttributes Attributes { get; set; } = new AdminAttributes();
    /// <summary>
    /// The dictionaries icon.
    /// </summary>
    public Icon AttributesIcon = new Icons.Regular.Size24.AppsListDetail();

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
    public Icon AdministrationIcon = new Icons.Regular.Size24.WindowDevTools();
}

