// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Application
// *************************************************

using Undersoft.SCC.Service.Contracts;
using Undersoft.SCC.Service.Contracts.Countries;
using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SCC.Service.Application.ViewModels.Contacts;

/// <summary>
/// The contact address.
/// </summary>
public class ContactAddress : DataObject, IViewModel
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
    public Country? Country { get; set; }

    /// <summary>
    /// Gets or sets the country name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
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
    public CountryState? CountryState { get; set; }

    /// <summary>
    /// Gets or sets the state name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
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