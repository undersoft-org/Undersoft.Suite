// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Application
// *************************************************

using Undersoft.SCC.Service.Contracts.Settings;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Setting;

namespace Undersoft.SCC.Service.Contracts.Details;

/// <summary>
/// The contact address.
/// </summary>
[Detail]
public class Address : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the country name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Setting]
    public Country? Country { get; set; }

    /// <summary>
    /// Gets or sets the country name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Setting]
    public CountryState? State { get; set; }

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the postcode.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? Postcode { get; set; }

    /// <summary>
    /// Gets or sets the street.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? Street { get; set; }

    /// <summary>
    /// Gets or sets the building.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? Building { get; set; }

    /// <summary>
    /// Gets or sets the apartment.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? Apartment { get; set; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    /// <value>A <see cref="string? "/></value>    
    public string? Notes { get; set; }

}