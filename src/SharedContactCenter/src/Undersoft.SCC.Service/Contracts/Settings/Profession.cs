// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Object.Setting;

namespace Undersoft.SCC.Service.Contracts.Settings;

/// <summary>
/// The contact professional.
/// </summary>
[Setting]
public class Profession : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the profession.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? Name { get; set; } = default!;

    public virtual string? Description { get; set; }

}
