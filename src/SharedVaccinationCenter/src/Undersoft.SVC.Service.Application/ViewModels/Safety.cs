// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

namespace Undersoft.SVC.Service.Application.ViewModels;

/// <summary>
/// The contact address.
/// </summary>
public class Safety : DataObject, IViewModel
{
    public int? ExpirationDays { get; set; }

    public string? Description { get; set; }

    public float? Temperature { get; set; }

    public long? VaccineId { get; set; }
}