// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts;

/// <summary>
/// The contact professional.
/// </summary>
public class Profession : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the profession.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? Name { get; set; } = default!;

    public virtual string? Description { get; set; }

}
