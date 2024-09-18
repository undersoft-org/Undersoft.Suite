// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application.GUI
// ********************************************************

using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SVC.Service.Application.GUI.Compound.Presenting.NavMenu.User;

public class UserCatalogs : DataObject
{
    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("CampaignsIcon")]
    [DisplayRubric("Campaigns")]
    public string Campaigns { get; set; } = "/presenting/user/catalogs/campaigns";
    public Icon CampaignsIcon = new Icons.Regular.Size20.LayerDiagonalPerson();

    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("VaccinesIcon")]
    [DisplayRubric("Vaccines")]
    public string Vaccines { get; set; } = "/presenting/user/catalogs/vaccines";
    public Icon VaccinesIcon = new Icons.Regular.Size20.DrinkBottle();

    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("ManufacturersIcon")]
    [DisplayRubric("Manufacturers")]
    public string Manufacturers { get; set; } = "/presenting/user/catalogs/manufacturers";
    public Icon ManufacturersIcon = new Icons.Regular.Size20.BuildingFactory();

    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("SuppliersIcon")]
    [DisplayRubric("Suppliers")]
    public string Suppliers { get; set; } = "/presenting/user/catalogs/suppliers";
    public Icon SuppliersIcon = new Icons.Regular.Size20.VehicleTruckCube();

    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("OfficesIcon")]
    [DisplayRubric("Offices")]
    public string Offices { get; set; } = "/presenting/user/catalogs/offices";
    public Icon OfficesIcon = new Icons.Regular.Size20.ConferenceRoom();
}

