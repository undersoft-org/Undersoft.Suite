// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Microsoft.OData.ModelBuilder;
using Undersoft.SCC.Domain.Entities.Countries;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Contacts;

/// <summary>
/// The contact address.
/// </summary>
public class ContactAddress : DataObject, IContract
{
    /// <summary>
    /// Gets or sets the country id.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public long? CountryId { get; set; }

    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Extended]
    [AutoExpand]
    public Country? Country { get; set; }

    /// <summary>
    /// Gets or sets the country name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? CountryName { get; set; }

    /// <summary>
    /// Gets or sets the state id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? CountryStateId { get; set; }

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Extended]
    [AutoExpand]
    public CountryState? CountryState { get; set; }

    /// <summary>
    /// Gets or sets the country name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the postcode.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    public string? Postcode { get; set; }

    /// <summary>
    /// Gets or sets the street.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    public string? Street { get; set; }

    /// <summary>
    /// Gets or sets the building.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    public string? Building { get; set; }

    /// <summary>
    /// Gets or sets the apartment.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    public string? Apartment { get; set; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the contact id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? ContactId { get; set; }
}