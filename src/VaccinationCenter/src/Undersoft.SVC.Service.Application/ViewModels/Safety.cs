// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SVC.Service.Application.ViewModels;

/// <summary>
/// The contact address.
/// </summary>
public class Safety : DataObject, IViewModel
{

    [VisibleRubric]
    [DisplayRubric("Expiration days")]
    public int? ExpirationDays { get; set; }

    [VisibleRubric]
    [DisplayRubric("Description")]
    public string? Description { get; set; }

    [VisibleRubric]
    [DisplayRubric("Temperature")]
    public float? Temperature { get; set; }

    public long? VaccineId { get; set; }
}