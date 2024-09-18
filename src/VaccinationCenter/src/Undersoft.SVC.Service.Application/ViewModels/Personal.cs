// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Service.Application.ViewModels;

public class Personal : DataObject, IViewModel
{
    [VisibleRubric]
    [DisplayRubric("ID type")]
    public IdentifierType IdentifierType { get; set; }

    [VisibleRubric]
    [DisplayRubric("ID")]
    public string? Identifier { get; set; } = default!;

    [VisibleRubric]
    [DisplayRubric("First name")]
    public string FirstName { get; set; } = default!;

    [VisibleRubric]
    [DisplayRubric("Last name")]
    public string LastName { get; set; } = default!;

    [VisibleRubric]
    [DisplayRubric("Email")]
    public string Email { get; set; } = default!;

    [VisibleRubric]
    [DisplayRubric("Phone number")]
    public string PhoneNumber { get; set; } = default!;

    [VisibleRubric]
    [DisplayRubric("Day of birth")]
    public DateTime Birthdate { get; set; } = DateTime.UtcNow;

    public long? AppointmentId { get; set; }

    public virtual long? CertificateId { get; set; }

    public virtual long? PostSymptomId { get; set; }
}
