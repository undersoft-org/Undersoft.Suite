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
/// The attributes.
/// </summary>
public class Attributes : DataObject
{
    /// <summary>
    /// Gets or sets the groups.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("GroupsIcon")]
    [DisplayRubric("Groups")]
    public string Groups { get; set; } = "/presenting/attributes/groups";
    /// <summary>
    /// The groups icon.
    /// </summary>
    public Icon GroupsIcon = new Icons.Regular.Size20.Group();

    /// <summary>
    /// Gets or sets the countries.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("DetailsIcon")]
    [DisplayRubric("Details")]
    public string Details { get; set; } = "/presenting/attributes/details";
    /// <summary>
    /// The countries icon.
    /// </summary>
    public Icon DetailsIcon = new Icons.Regular.Size20.ContentView();

    /// <summary>
    /// Gets or sets the countries.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("SettingsIcon")]
    [DisplayRubric("Settings")]
    public string Settings { get; set; } = "/presenting/attributes/settings";
    /// <summary>
    /// The countries icon.
    /// </summary>
    public Icon SettingsIcon = new Icons.Regular.Size20.ContentSettings();

}

