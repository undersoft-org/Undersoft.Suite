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

/// <summary>
/// The contacts.
/// </summary>
public class UserInventory : DataObject
{
    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("StocksIcon")]
    [DisplayRubric("Stocks")]
    public string Stocks { get; set; } = "/presenting/user/inventory/stocks";
    public Icon StocksIcon = new Icons.Regular.Size20.Box();

    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("RequestsIcon")]
    [DisplayRubric("Requests")]
    public string Requests { get; set; } = "/presenting/user/inventory/requests";
    public Icon RequestsIcon = new Icons.Regular.Size20.BoxArrowUp();

    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("TrafficsIcon")]
    [DisplayRubric("Traffics")]
    public string Traffics { get; set; } = "/presenting/user/inventory/traffics";
    public Icon TrafficsIcon = new Icons.Regular.Size20.CubeSync();
}

