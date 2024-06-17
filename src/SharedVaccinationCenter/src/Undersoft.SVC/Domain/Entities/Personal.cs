// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Domain.Entities;

public class Personal : Entity
{
    public IdentifierType IdentifierType { get; set; }

    public string? Identifier { get; set; } = default!;

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public DateTime Birthdate { get; set; } = DateTime.UtcNow;

    public long? AppointmentId { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual long? CertificateId { get; set; }

    public virtual Certificate? Certificate { get; set; }

    public virtual long? PostSymptomId { get; set; }

    public virtual PostSymptom? PostSymptom { get; set; }

}
