using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Undersoft.SDK.Instant.Attributes;
using Undersoft.SDK.Logging;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public class Origin : Identifiable, IOrigin, IUnique
    {
        public Origin() : base(true)
        {
        }

        public Origin(bool autoId) : base(autoId) { }

        [IdentityRubric(Order = 2)]
        [DataMember(Order = 3)]
        [Column(Order = 3)]
        public virtual long OriginId
        {
            get
            {
                return GetOriginId();
            }
            set
            {
                SetOriginId(value);
            }
        }

        [Column(TypeName = "timestamp", Order = 6)]
        [DataMember(Order = 6)]
        [InstantAs(UnmanagedType.I8, SizeConst = 8)]
        public virtual DateTime Modified { get; set; }

        [StringLength(128)]
        [Column(Order = 7)]
        [DataMember(Order = 7)]
        [InstantAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public virtual string Modifier { get; set; } = "";

        [Column(TypeName = "timestamp", Order = 8)]
        [DataMember(Order = 8)]
        [InstantAs(UnmanagedType.I8, SizeConst = 8)]
        public virtual DateTime Created { get; set; }

        [StringLength(128)]
        [Column(Order = 9)]
        [DataMember(Order = 9)]
        [InstantAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public virtual string Creator { get; set; } = "";

        [DataMember(Order = 10)]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Index { get; set; } = -1;

        [Column(Order = 11)]
        [StringLength(256)]
        [DataMember(Order = 11)]
        [InstantAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public virtual string Label { get; set; } = "";

        public virtual TEntity Sign<TEntity>(TEntity entity = null) where TEntity : class, IOrigin
        {
            if (entity == null)
            {
                AutoId();
                if (!HaveTime())
                    Time = Log.Clock;
                Stamp<TEntity>();
                Created = Time;
                return default;
            }
            entity.AutoId();
            if (!HaveTime())
                entity.Time = Log.Clock;
            Stamp(entity);
            entity.Created = entity.Time;
            return entity;
        }

        public virtual TEntity Stamp<TEntity>(TEntity entity = null) where TEntity : class, IOrigin
        {
            if (entity == null)
            {
                Modified = Log.Clock;
                return default;
            }
            entity.Modified = Log.Clock;
            return entity;

        }

        public virtual bool Equals(IUnique other)
        {
            return Id.Equals(other.Id);
        }

        public virtual int CompareTo(IUnique other)
        {
            return Id.CompareTo(other.Id);
        }
    }



}
