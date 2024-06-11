// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts;

using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SCC.Service.Application.ViewModels;
using Undersoft.SCC.Service.Contracts.Details;
using Undersoft.SCC.Service.Contracts.Detailsl;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Object.Detail;

/// <summary>
/// The contact.
/// </summary>
public class MemberEdge : OpenContract<Member, Detail, Setting, Group>
{

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    [Identify]
    public virtual string? Name { get => Label; set => Label = value; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>A <see cref="MemberKind"/></value>
    [Identify]
    public virtual MemberKind Kind { get; set; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    /// <value>A <see cref="string? "/></value>
    public virtual string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the personal.
    /// </summary>
    /// <value>A <see cref="Contacts.Personal? "/></value>
    [Detail]
    public virtual Personal? Personal { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <value>A <see cref="ContactAddress? "/></value>
    [Detail]
    public virtual Address? Address { get; set; }

    /// <summary>
    /// Gets or sets the professional.
    /// </summary>
    /// <value>A <see cref="Contacts.Professional? "/></value>
    [Detail]
    public virtual Professional? Professional { get; set; }

    /// <summary>
    /// Gets or sets the organization.
    /// </summary>
    /// <value>A <see cref="ViewModels.Organization? "/></value>
    [Detail]
    public virtual Organization? Organization { get; set; }
}
