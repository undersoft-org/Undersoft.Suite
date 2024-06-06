// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Application
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SCC.Service.Application.ViewModels.Contacts;

/// <summary>
/// The contact address.
/// </summary>
public class ContactAddress : DataObject, IViewModel
{
    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [VisibleRubric]
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the state.
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