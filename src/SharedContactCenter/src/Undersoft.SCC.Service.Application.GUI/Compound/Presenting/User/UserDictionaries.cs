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
/// The user dictionaries.
/// </summary>
public class UserDictionaries : DataObject
{
    /// <summary>
    /// Gets or sets the groups.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Groups")]
    public string Groups { get; set; } = "/presenting/user/dictionaries/groups";
    /// <summary>
    /// The groups icon.
    /// </summary>
    public Icon GroupsIcon = new Icons.Regular.Size20.ContactCardGroup();

    /// <summary>
    /// Gets or sets the countries.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Details")]
    public string Details { get; set; } = "/presenting/user/dictionaries/details";
    /// <summary>
    /// The countries icon.
    /// </summary>
    public Icon CountriesIcon = new Icons.Regular.Size20.Flag();

    /// <summary>
    /// Gets or sets the countries.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Settings")]
    public string Settings { get; set; } = "/presenting/user/dictionaries/settings";
    /// <summary>
    /// The countries icon.
    /// </summary>
    public Icon SettingsIcon = new Icons.Regular.Size20.Flag();
}

