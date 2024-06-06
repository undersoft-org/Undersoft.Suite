// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

namespace Undersoft.SCC.Domain.Entities.Contacts;

using Undersoft.SCC.Domain.Entities.Enums;

/// <summary>
/// The contact organization.
/// </summary>
public class ContactOrganization : Entity
{
    /// <summary>
    /// Gets or sets the organization industry.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? OrganizationIndustry { get; set; }

    /// <summary>
    /// Gets or sets the organization name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? OrganizationName { get; set; }

    /// <summary>
    /// Gets or sets the organization full name.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? OrganizationFullName { get; set; }

    /// <summary>
    /// Gets or sets the position in organization.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? PositionInOrganization { get; set; }

    /// <summary>
    /// Gets or sets the organization websites.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? OrganizationWebsites { get; set; }

    /// <summary>
    /// Gets or sets the organization size.
    /// </summary>
    /// <value>An <see cref="OrganizationSize"/></value>
    public OrganizationSize OrganizationSize { get; set; }

    /// <summary>
    /// Gets or sets the organization image.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public string? OrganizationImage { get; set; }

    /// <summary>
    /// Gets or sets the organization image data.
    /// </summary>
    /// <value>A <see cref="byte[]? "/></value>
    public byte[]? OrganizationImageData { get; set; }

    /// <summary>
    /// Gets or sets the contact id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? ContactId { get; set; }
    /// <summary>
    /// Gets or sets the contact.
    /// </summary>
    /// <value>A <see cref="Contact? "/></value>
    public virtual Contact? Contact { get; set; }
}
