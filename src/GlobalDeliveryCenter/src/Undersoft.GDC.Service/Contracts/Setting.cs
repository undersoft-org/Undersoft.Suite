using System.Runtime.Serialization;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Object.Setting;
using Undersoft.GDC.Domain.Entities.Enums;

namespace Undersoft.GDC.Service.Contracts
{
    [DataContract]
    public class Setting : ObjectSetting<Setting, SettingKind>, ISetting, IContract
    {
        public Setting() : base() { }

        public Setting(SettingKind kind) : base(kind) { }
    }
}
