// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

namespace Undersoft.SCC.Domain.Entities;

using Undersoft.SCC.Domain.Entities.Countries;

/// <summary>
/// The country.
/// </summary>
public partial class Country : Entity
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the country code.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
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
    public string? TimeZone { get; set; }

    /// <summary>
    /// Gets or sets the country image.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? CountryImage { get; set; }

    /// <summary>
    /// Gets or sets the country image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    public byte[]? CountryImageData { get; set; }

    /// <summary>
    /// Gets or sets the currency id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? CurrencyId { get; set; }
    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    /// <value>A <see cref="Currency? "/></value>
    public virtual Currency? Currency { get; set; }

    /// <summary>
    /// Gets or sets the language id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? LanguageId { get; set; }
    /// <summary>
    /// Gets or sets the language.
    /// </summary>
    /// <value>A <see cref="CountryLanguage? "/></value>
    public virtual CountryLanguage? Language { get; set; }

    /// <summary>
    /// Gets or sets the states.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public virtual EntitySet<CountryState>? States { get; set; }
}
