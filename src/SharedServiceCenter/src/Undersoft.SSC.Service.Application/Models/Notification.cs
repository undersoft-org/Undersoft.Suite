using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Models;

public class Notification : Member, IViewModel
{
    public Notification() { Group = MemberGroup.Broker; }

}
