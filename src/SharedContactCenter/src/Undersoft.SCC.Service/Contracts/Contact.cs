// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts;

using Microsoft.OData.ModelBuilder;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SCC.Service.Contracts.Contacts;
using Undersoft.SDK.Rubrics.Attributes;

/// <summary>
/// The contact.
/// </summary>
public class Contact : OpenContract<Contact, Detail, Setting, Group>
{

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>A <see cref="ContactType"/></value>
    public virtual ContactType Type { get; set; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public virtual string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the personal id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? PersonalId { get; set; }

    /// <summary>
    /// Gets or sets the personal.
    /// </summary>
    /// <value>A <see cref="ContactPersonal? "/></value>
    [Extended]
    [AutoExpand]
    public virtual ContactPersonal? Personal { get; set; }

    /// <summary>
    /// Gets or sets the address id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? AddressId { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <value>A <see cref="Contacts.ContactAddress? "/></value>
    [Extended]
    [AutoExpand]
    public virtual ContactAddress? Address { get; set; }

    /// <summary>
    /// Gets or sets the professional id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? ProfessionalId { get; set; }

    /// <summary>
    /// Gets or sets the professional.
    /// </summary>
    /// <value>A <see cref="ContactProfessional? "/></value>
    [Extended]
    [AutoExpand]
    public virtual ContactProfessional? Professional { get; set; }

    /// <summary>
    /// Gets or sets the organization id.
    /// </summary>
    /// <value>A <see cref="long? "/></value>
    public long? OrganizationId { get; set; }

    /// <summary>
    /// Gets or sets the organization.
    /// </summary>
    /// <value>A <see cref="Contracts.Organization? "/></value>
    [Extended]
    [AutoExpand]
    public virtual Organization? Organization { get; set; }
}
