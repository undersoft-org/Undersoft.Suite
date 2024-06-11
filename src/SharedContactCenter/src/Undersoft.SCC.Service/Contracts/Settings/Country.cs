
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
/// The country.
/// </summary>
[Setting]
public partial class Country : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the country image.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? CountryImage { get; set; } = default!;

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the country code.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? CountryCode { get; set; }

    /// <summary>
    /// Gets or sets the continent.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? Continent { get; set; }

    /// <summary>
    /// Gets or sets the time zone.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public string? TimeZone { get; set; }

    /// <summary>
    /// Gets or sets the language name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public int? PhonePrefix { get; set; }

    /// <summary>
    /// Gets or sets the country image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    public byte[]? CountryImageData { get; set; } = default!;

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    /// <value>A <see cref="Currency? "/></value>
    [Setting]
    public virtual Currency? Currency { get; set; }


    /// <summary>
    /// Gets or sets the language.
    /// </summary>
    /// <value>A <see cref="Settings.Language? "/></value>
    [Setting]
    public virtual Language? Language { get; set; }

    /// <summary>
    /// Gets or sets the states.
    /// </summary>
    /// <value>A TODO: Add missing XML "/&gt;</value>
    [Settings]
    public virtual Listing<CountryState>? States { get; set; }
}
