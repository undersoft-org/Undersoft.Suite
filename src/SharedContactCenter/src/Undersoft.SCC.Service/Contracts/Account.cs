using Microsoft.OData.ModelBuilder;
using System.Runtime.Serialization;
using Undersoft.SCC.Service.Contracts.Accounts;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Contracts;

[DataContract]
[Validator("AccountValidator")]
public class Account : Authorization, IContract
{
    public Account() { }

    public Account(string email) { Id = email.UniqueKey64(); }

    [VisibleRubric]
    [DisplayRubric("Id")]
    [DataMember(Order = 1)]
    public override long Id { get => base.Id; set => base.Id = value; }

    [VisibleRubric]
    [RubricSize(256)]
    [DisplayRubric("Label")]
    [DataMember(Order = 11)]
    public override string? Label { get => base.Label; set => base.Label = value; }

    [DataMember(Order = 12)]
    public AccountUser? User { get; set; } = default!;

    [DataMember(Order = 13)]
    public Listing<Role>? Roles { get; set; } = default!;

    [DataMember(Order = 14)]
    public Listing<Claim>? Claims { get; set; } = default!;

    [DataMember(Order = 16)]
    public long? PersonalId { get; set; }

    [Expand]
    [Extended]
    [DataMember(Order = 17)]
    public virtual AccountPersonal Personal { get; set; } = default!;

    [DataMember(Order = 18)]
    public long? AddressId { get; set; }

    [Expand]
    [Extended]
    [DataMember(Order = 19)]
    public virtual AccountAddress Address { get; set; } = default!;

    [DataMember(Order = 20)]
    public long? ProfessionalId { get; set; }

    [Expand]
    [Extended]
    [DataMember(Order = 21)]
    public virtual AccountProfessional Professional { get; set; } = default!;

    [DataMember(Order = 22)]
    public long? OrganizationId { get; set; }

    [Expand]
    [Extended]
    [DataMember(Order = 23)]
    public virtual AccountOrganization Organization { get; set; } = default!;
}
