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
/// The currency.
/// </summary>
[Setting]
public partial class Currency : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the currency code.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? CurrencyCode { get; set; }
}
