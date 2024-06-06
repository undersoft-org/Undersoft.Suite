using Microsoft.OData.ModelBuilder;

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
using Undersoft.SCC.Service.Contracts.Contacts;

public class Contact : DataObject, IContract
{

    public virtual ContactType Type { get; set; }

    public virtual string? Notes { get; set; }

    public long? PersonalId { get; set; }

    [AutoExpand]
    public virtual ContactPersonal? Personal { get; set; } = default!;

    public long? AddressId { get; set; }

    [AutoExpand]
    public virtual ContactAddress? Address { get; set; } = default!;

    public long? ProfessionalId { get; set; }

    [AutoExpand]
    public virtual ContactProfessional? Professional { get; set; } = default!;

    public long? OrganizationId { get; set; }

    [AutoExpand]
    public virtual ContactOrganization? Organization { get; set; } = default!;

    [AutoExpand]
    public virtual Listing<Group>? Groups { get; set; } = default!;
}
