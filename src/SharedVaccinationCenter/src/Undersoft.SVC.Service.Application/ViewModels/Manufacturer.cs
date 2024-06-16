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
public class Manufacturer : DataObject, IViewModel
{
    public string? ManufacturerImage { get; set; }

    public byte[]? ManufacturerImageData { get; set; }

    public string? FullName { get; set; }

    public string? Name { get; set; }
}