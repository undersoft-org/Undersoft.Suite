// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Object.Setting;

namespace Undersoft.SCC.Service.Contracts.Settings;

/// <summary>
/// The country state.
/// </summary>
[Setting]
public class CountryState : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the state code.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? StateCode { get; set; }

    /// <summary>
    /// Gets or sets the time zone.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? TimeZone { get; set; }
}
