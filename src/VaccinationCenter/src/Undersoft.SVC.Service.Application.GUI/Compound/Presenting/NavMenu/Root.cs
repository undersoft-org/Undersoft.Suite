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

namespace Undersoft.SVC.Service.Application.GUI.Compound.Presenting.NavMenu;

public class Root : DataObject
{
    [MenuGroup]
    [Extended]
    [IconRubric("DashboardIcon")]
    [Link("/presenting/dashboard")]
    public Dashboard Dashboard { get; set; } = new Dashboard();
    public Icon DashboardIcon = new Icons.Regular.Size24.ChartMultiple();

    [MenuGroup]
    [Extended]
    [IconRubric("VaccinationIcon")]
    public Vaccination Vaccination { get; set; } = new Vaccination();
    public Icon VaccinationIcon = new Icons.Regular.Size24.Syringe();
}

