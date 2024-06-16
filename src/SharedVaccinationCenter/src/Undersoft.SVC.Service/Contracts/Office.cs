// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SVC.Service.Contracts;

/// <summary>
/// The contact address.
/// </summary>
public class Office : DataObject, IContract
{
    public string? Number { get; set; }

    public string? Name { get; set; }
}