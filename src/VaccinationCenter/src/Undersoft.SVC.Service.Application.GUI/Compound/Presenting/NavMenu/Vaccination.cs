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

namespace Undersoft.SVC.Service.Application.GUI.Compound.Presenting.NavMenu;

public class Vaccination : DataObject
{
    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("AppointmentsIcon")]
    [DisplayRubric("Appointments")]
    public string Appointments { get; set; } = "/presenting/vaccination/appointments";
    public Icon AppointmentsIcon = new Icons.Regular.Size20.CalendarClock();
}

