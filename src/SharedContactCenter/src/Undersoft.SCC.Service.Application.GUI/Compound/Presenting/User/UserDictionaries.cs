﻿using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Presenting.User;

public class UserDictionaries : DataObject
{
    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Groups")]
    public string Groups { get; set; } = "/presenting/user/dictionaries/groups";
    public Icon GroupsIcon = new Icons.Regular.Size20.ContactCardGroup();

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Countries")]
    public string Countries { get; set; } = "/presenting/user/dictionaries/countries";
    public Icon CountriesIcon = new Icons.Regular.Size20.Flag();
}
