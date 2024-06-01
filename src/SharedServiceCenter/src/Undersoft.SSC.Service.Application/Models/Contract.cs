using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;
using Undersoft.SSC.Service.Contracts.Details;

namespace Undersoft.SSC.Service.Application.Models;

public class Contract : Member, IViewModel
{
    public Contract() { Group = MemberGroup.Organization; }

    [Detail]
    public Contracts.Account? Identity { get; set; }

    [Detail]
    public Personal? Personal { get; set; }

    [Detail]
    public Expression? Company { get; set; }

    [Detail]
    public ObjectSet<Contracts.Details.Request>? Employees { get; set; }

    [Detail]
    public ObjectSet<Contracts.Details.Response>? Licences { get; set; }
}
