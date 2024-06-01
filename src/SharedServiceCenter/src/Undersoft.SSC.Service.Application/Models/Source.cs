using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Models;

public class Source : Member, IViewModel
{
    public Source() { Group = MemberGroup.Servitizer; }

}
