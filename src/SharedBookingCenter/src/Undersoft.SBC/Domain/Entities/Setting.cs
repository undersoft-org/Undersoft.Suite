using Undersoft.SBC.Domain.Entities.Enums;
using Undersoft.SDK.Serialization;
using Undersoft.SDK.Service.Data.Object.Setting;

namespace Undersoft.SBC.Domain.Entities
{
    public class Setting : ObjectSetting<Setting, SettingKind>, IEntity, IJsonDocumentSerializable, ISetting
    {
        public virtual EntitySet<Setting>? RelatedFrom { get; set; }

        public virtual EntitySet<Setting>? RelatedTo { get; set; }

        public virtual EntitySet<Activity>? Activities { get; set; }

        public virtual EntitySet<Member>? Members { get; set; }

        public virtual EntitySet<Resource>? Resources { get; set; }

        public virtual EntitySet<Schedule>? Schedules { get; set; }

        public virtual EntitySet<Service>? Services { get; set; }
    }
}