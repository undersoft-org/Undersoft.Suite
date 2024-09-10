// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

namespace Undersoft.SVC.Service.Application.ViewModels;

public class Professional : DataObject, IViewModel
{

    public string? ProfessionalImage { get; set; }

    public string? ProfessionalManager { get; set; }

    public string? ProfessionalName { get; set; }

    public string? ProfessionalPosition { get; set; }

    public string? ProfessionalEmail { get; set; }

    public string? ProfessionalPhoneNumber { get; set; }

    public string? ProfessionalWebsites { get; set; }

    public float? ProfessionalExperience { get; set; }

    public byte[]? ProfessionalImageData { get; set; }

    public long? SupplierId { get; set; }
}
