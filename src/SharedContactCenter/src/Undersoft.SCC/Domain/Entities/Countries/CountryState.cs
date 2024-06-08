// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

using Undersoft.SCC.Domain.Entities.Contacts;

namespace Undersoft.SCC.Domain.Entities.Countries;

/// <summary>
/// The country state.
/// </summary>
public class CountryState : Entity
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the state code.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? StateCode { get; set; }

    /// <summary>
    /// Gets or sets the time zone.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? TimeZone { get; set; }

    /// <summary>
    /// Gets or sets the country id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? CountryId { get; set; }
    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    /// <value>A <see cref="Country? "/></value>
    public virtual Country? Country { get; set; }

    /// <summary>
    /// Gets or sets the groups.
    /// </summary>
    /// <value>An EntitySet&lt;Group&gt;?</value>
    public virtual EntitySet<ContactAddress>? Addresses { get; set; }
}
