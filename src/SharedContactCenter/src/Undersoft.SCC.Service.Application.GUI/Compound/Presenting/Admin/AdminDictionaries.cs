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
/// The admin dictionaries.
/// </summary>
public class AdminDictionaries : DataObject
{
    /// <summary>
    /// Gets or sets the groups.
    /// </summary>
    /// <value>A <see cref="string"/></value>
    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Groups")]
    public string Groups { get; set; } = "/presenting/admin/dictionaries/groups";
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
    [DisplayRubric("Countries")]
    public string Countries { get; set; } = "/presenting/admin/dictionaries/countries";
    /// <summary>
    /// The countries icon.
    /// </summary>
    public Icon CountriesIcon = new Icons.Regular.Size20.Flag();
}

