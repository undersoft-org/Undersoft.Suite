using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Models;

public class Work : Member, IViewModel
{
    public Work() { Group = MemberGroup.Servitizer; }

}
