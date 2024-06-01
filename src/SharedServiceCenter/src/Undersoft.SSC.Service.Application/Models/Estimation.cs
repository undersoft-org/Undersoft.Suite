using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Models;

public class Estimation : Member, IViewModel
{
    public Estimation() { Group = MemberGroup.Servitizer; }

}
