using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Models;

public class Workflow : ModelBase<Service, Detail, Setting, MemberGroup>, IViewModel
{
    public Workflow() { Group = MemberGroup.Servitizer; }

    [Detail]
    public Contracts.Account? Identity { get; set; }
}
