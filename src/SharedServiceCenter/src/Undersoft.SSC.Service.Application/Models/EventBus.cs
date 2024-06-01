using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Models;

public class EventBus : Member, IViewModel
{
    public EventBus() { Group = MemberGroup.Servitizer; }

}
