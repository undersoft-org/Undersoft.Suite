using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Setting;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;
using Undersoft.SSC.Service.Contracts.Settings;

namespace Undersoft.SSC.Service.Application.Models;

public class Client : ModelBase<Client, Detail, Setting, MemberGroup>, IViewModel
{
    public Client() { Group = MemberGroup.Client; }

    [Detail]
    public Contracts.Account? Identity { get; set; }

    [Setting]
    public ProfileSetting? Profile { get; set; }
}
