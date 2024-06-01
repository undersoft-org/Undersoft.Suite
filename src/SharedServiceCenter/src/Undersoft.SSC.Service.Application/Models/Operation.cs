using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Models;

public class Operation : Member, IViewModel
{
    public Operation() { Group = MemberGroup.Servitizer; }

}
