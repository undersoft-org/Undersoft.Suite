using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Setting;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;
using Undersoft.SSC.Service.Contracts.Details;
using Undersoft.SSC.Service.Contracts.Settings;

namespace Undersoft.SSC.Service.Application.Models;

public class User : ModelBase<User, Detail, Setting, MemberGroup>, IViewModel
{
    public User() { Group = MemberGroup.User; }

    [Detail]
    public Contracts.Account? Identity { get; set; }

    [Detail]
    public Personal? Personal { get; set; }

    [Setting]
    public ProfileSetting? Profile { get; set; }

    [Detail]
    public ObjectSet<Contracts.Details.Response>? Licences { get; set; }
}
