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

public class AdminRoot : DataObject
{
    [MenuGroup]
    [Extended]
    [IconRubric("DashboardIcon")]
    [Link("/presenting/admin/dashboard")]
    public AdminDashboard Dashboard { get; set; } = new AdminDashboard();
    public Icon DashboardIcon = new Icons.Regular.Size24.ChartMultiple();

    [MenuGroup]
    [Extended]
    [IconRubric("VaccinationIcon")]
    public AdminVaccination Vaccination { get; set; } = new AdminVaccination();
    public Icon VaccinationIcon = new Icons.Regular.Size24.Syringe();

    [MenuGroup]
    [Extended]
    [IconRubric("CatalogsIcon")]
    public AdminCatalogs Catalogs { get; set; } = new AdminCatalogs();
    public Icon CatalogsIcon = new Icons.Regular.Size24.AppsListDetail();

    [MenuGroup]
    [Extended]
    [IconRubric("InventoryIcon")]
    public AdminInventory Inventory { get; set; } = new AdminInventory();
    public Icon InventoryIcon = new Icons.Regular.Size24.BoxMultiple();

    [MenuGroup]
    [Extended]
    [IconRubric("AdministrationIcon")]
    public AdminAdministration Administration { get; set; } = new AdminAdministration();
    public Icon AdministrationIcon = new Icons.Regular.Size24.WindowDevTools();
}

