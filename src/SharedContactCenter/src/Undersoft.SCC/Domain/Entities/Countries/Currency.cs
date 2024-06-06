// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

namespace Undersoft.SCC.Domain.Entities.Countries;

/// <summary>
/// The currency.
/// </summary>
public partial class Currency : Entity
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the currency code.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? CurrencyCode { get; set; }

    /// <summary>
    /// Gets or sets the countries.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public virtual EntitySet<Country>? Countries { get; set; }
}
