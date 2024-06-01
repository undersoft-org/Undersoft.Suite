using Undersoft.SDK.Serialization;
using Undersoft.SDK.Service.Data.Object.Setting;
using Undersoft.SSC.Domain.Entities.Enums;

namespace Undersoft.SSC.Domain.Entities
{
    public class Setting : ObjectSetting<Setting, SettingKind>, IEntity, ISerializableJsonDocument, ISetting
    {
        public virtual EntitySet<Setting>? RelatedFrom { get; set; }

        public virtual EntitySet<Setting>? RelatedTo { get; set; }

        public virtual EntitySet<Activity>? Activities { get; set; }

        public virtual EntitySet<Member>? Members { get; set; }

        public virtual EntitySet<Resource>? Resources { get; set; }

        public virtual EntitySet<Schedule>? Schedules { get; set; }

        public virtual EntitySet<Application>? Applications { get; set; }


        public virtual EntitySet<Service>? Services { get; set; }

        public virtual long DefaultId { get; set; }
        public virtual Default? Default { get; set; }
    }
}