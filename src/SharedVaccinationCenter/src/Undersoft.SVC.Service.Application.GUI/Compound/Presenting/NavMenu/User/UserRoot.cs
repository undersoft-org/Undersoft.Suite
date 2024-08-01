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

namespace Undersoft.SVC.Service.Application.GUI.Compound.Presenting.NavMenu.User;

public class UserRoot : DataObject
{
    [MenuGroup]
    [Extended]
    [IconRubric("DashboardIcon")]
    [Link("/presenting/user/dashboard")]
    public UserDashboard Dashboard { get; set; } = new UserDashboard();
    public Icon DashboardIcon = new Icons.Regular.Size24.ChartMultiple();

    [MenuGroup]
    [Extended]
    [IconRubric("VaccinationIcon")]
    public UserVaccination Vaccination { get; set; } = new UserVaccination();
    public Icon VaccinationIcon = new Icons.Regular.Size24.Syringe();

    [MenuGroup]
    [Extended]
    [IconRubric("CatalogsIcon")]
    public UserCatalogs Catalogs { get; set; } = new UserCatalogs();
    public Icon CatalogsIcon = new Icons.Regular.Size24.AppsListDetail();

    [MenuGroup]
    [Extended]
    [IconRubric("InventoryIcon")]
    public UserInventory Inventory { get; set; } = new UserInventory();
    public Icon InventoryIcon = new Icons.Regular.Size24.BoxMultiple();
}

