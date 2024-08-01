using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Setting;
using Undersoft.SSC.Service.Contracts;
using Undersoft.SSC.Service.Contracts.Settings;

namespace Undersoft.SSC.Service.Application.Models;

public class Client : ModelBase<Client, Detail, Setting, Group>, IViewModel
{
    public Client() { }

    [Detail]
    public Contracts.Account? Identity { get; set; }

    [Setting]
    public ProfileSetting? Profile { get; set; }
}
