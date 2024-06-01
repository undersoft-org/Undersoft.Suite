using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Models;

public class Computation : Member, IViewModel
{
    public Computation() { Group = MemberGroup.Servitizer; }

}
