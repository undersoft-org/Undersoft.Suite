using Microsoft.FluentUI.AspNetCore.Components;


// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application.GUI
// ********************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SVC.Service.Application.GUI.Compound.Presenting.NavMenu.Admin;

/// <summary>
/// The admin administration.
/// </summary>
public class AdminAdministration : DataObject
{
    /// <summary>
    /// Gets or sets the accounts.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Accounts")]
    [IconRubric("AccountsIcon")]
    public string Accounts { get; set; } = "/presenting/admin/administration/accounts";
    /// <summary>
    /// The accounts icon.
    /// </summary>
    public Icon AccountsIcon = new Icons.Regular.Size20.PersonAccounts();

    /// <summary>
    /// Gets or sets the events.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Events")]
    [IconRubric("EventsIcon")]
    public string Events { get; set; } = "/presenting/admin/administration/events";
    /// <summary>
    /// The events icon.
    /// </summary>
    public Icon EventsIcon = new Icons.Regular.Size20.TableLightning();
}

