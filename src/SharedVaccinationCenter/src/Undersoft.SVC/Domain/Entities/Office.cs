// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

namespace Undersoft.SVC.Domain.Entities;

/// <summary>
/// The contact address.
/// </summary>
public class Office : Entity
{
    public string? Number { get; set; }

    public string? Name { get; set; }

    public virtual EntitySet<Appointment>? Appointments { get; set; }
}