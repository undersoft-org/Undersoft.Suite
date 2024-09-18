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

public class UserVaccination : DataObject
{
    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("AppointmentsIcon")]
    [DisplayRubric("Appointments")]
    public string Appointments { get; set; } = "/presenting/user/vaccination/appointments";
    public Icon AppointmentsIcon = new Icons.Regular.Size20.CalendarClock();

    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("ProceduresIcon")]
    [DisplayRubric("Procedures")]
    public string Procedures { get; set; } = "/presenting/user/vaccination/procedures";
    public Icon ProceduresIcon = new Icons.Regular.Size20.Stethoscope();

    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("CertificatesIcon")]
    [DisplayRubric("Certificates")]
    public string Certificates { get; set; } = "/presenting/user/vaccination/Certificates";
    public Icon CertificatesIcon = new Icons.Regular.Size20.Certificate();

    [Link]
    [MenuItem]
    [Extended]
    [IconRubric("PostSymptomsIcon")]
    [DisplayRubric("Post symptoms")]
    public string PostSymptoms { get; set; } = "/presenting/user/vaccination/post_symptoms";
    public Icon PostSymptomsIcon = new Icons.Regular.Size20.HeartPulseWarning();
}

