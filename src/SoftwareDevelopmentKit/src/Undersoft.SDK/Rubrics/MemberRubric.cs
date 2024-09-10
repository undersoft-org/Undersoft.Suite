namespace Undersoft.SDK.Rubrics
{

    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using Undersoft.SDK;
    using Undersoft.SDK.Invoking;
    using Undersoft.SDK.Rubrics.Attributes;
    using Undersoft.SDK.Updating;
    using Uniques;

    public class MemberRubric : MemberInfo, IRubric
    {
        Uscn code = Uscn.Empty;
        IOrigin origin = new Origin();
        public object deltamark;
        public bool deltamarkset = false;
        public string label;

        public MemberRubric()
        {
        }

        public MemberRubric(FieldInfo field) : this((IMemberRubric)new FieldRubric(field)) { }

        public MemberRubric(FieldRubric field) : this((IMemberRubric)field) { }

        public MemberRubric(IMemberRubric member) : this()
        {
            RubricInfo = (MemberInfo)member;
            RubricName = member.RubricName;
            RubricId = member.RubricId;
            Visible = member.Visible;
            Editable = member.Editable;
            if (RubricInfo.MemberType == MemberTypes.Method)
                Id = $"{RubricName}_{new string(RubricParameterInfo.SelectMany(p => p.ParameterType.Name).ToArray())}"
                .UniqueKey64();
            else
                Id = RubricName.UniqueKey64();
        }

        public MemberRubric(MethodInfo method) : this((IMemberRubric)new MethodRubric(method)) { }

        public MemberRubric(MethodRubric method) : this((IMemberRubric)method) { }

        public MemberRubric(PropertyInfo property)
            : this((IMemberRubric)new PropertyRubric(property)) { }

        public MemberRubric(PropertyRubric property) : this((IMemberRubric)property) { }

        public MemberRubric(MemberRubric member, bool copyMode = false)
            : this(
                !copyMode && member.RubricInfo != null
                    ? (IMemberRubric)member.RubricInfo
                    : member
            )
        {
            InstantType = member.InstantType;
            InstantField = member.InstantField;
            FieldId = member.FieldId;
            RubricOffset = member.RubricOffset;
            IsKey = member.IsKey;
            IsIdentity = member.IsIdentity;
            IsAutoincrement = member.IsAutoincrement;
            IdentityOrder = member.IdentityOrder;
            Required = member.Required;
            DisplayName = member.DisplayName;
            RubricSize = member.RubricSize;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override object[] GetCustomAttributes(bool inherit)
        {
            return RubricInfo.GetCustomAttributes(inherit);
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return RubricInfo.GetCustomAttributes(attributeType, inherit);
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return RubricInfo.IsDefined(attributeType, inherit);
        }

        public MemberRubric ShallowCopy(MemberRubric dst)
        {
            if (dst == null)
                dst = new MemberRubric();
            this.ShallowPutTo(dst);
            dst.Id = RubricName.UniqueKey64();

            return dst;
        }

        public long AutoId()
        {
            return origin.AutoId();
        }

        public byte GetPriority()
        {
            return origin.GetPriority();
        }

        TEntity IOrigin.Sign<TEntity>(TEntity entity)
        {
            return origin.Sign(entity);
        }

        TEntity IOrigin.Stamp<TEntity>(TEntity entity)
        {
            return origin.Stamp(entity);
        }

        public long SetId(long id)
        {
            return origin.SetId(id);
        }

        public long SetId(object id)
        {
            return origin.SetId(id);
        }

        public bool Equals(IUnique other)
        {
            return ((IEquatable<IUnique>)code).Equals(other);
        }

        public int CompareTo(IUnique other)
        {
            return ((IComparable<IUnique>)code).CompareTo(other);
        }

        public void GetFlag(DataFlags state)
        {
            origin.GetFlag(state);
        }

        public void SetFlag(DataFlags state, bool flag)
        {
            origin.SetFlag(state, flag);
        }

        public override Type DeclaringType =>
            InstantType != null ? InstantType : RubricInfo.DeclaringType;

        public string DisplayName { get; set; }

        public bool Editable { get; set; }

        public IUnique Empty => Uscn.Empty;

        public int FieldId { get; set; }

        public FieldInfo InstantField { get; set; }

        public Type InstantType { get; set; }

        public MethodInfo Getter =>
            MemberType == MemberTypes.Property ? ((PropertyRubric)RubricInfo).GetMethod : null;

        public short IdentityOrder { get; set; }

        public bool IsAutoincrement { get; set; }

        public bool IsDBNull { get; set; }

        public bool Extended { get; set; }

        public bool Sortable { get; set; }

        public bool Filterable { get; set; }

        public bool Expanded { get; set; }

        public bool Disabled { get; set; }

        public bool IsIdentity { get; set; }

        public bool IsLink { get; set; }

        public bool IsKey { get; set; }

        public bool IsUnique { get; set; }

        public bool IsFile { get; set; }

        public FileRubricType FileType { get; set; }

        public string DataMember { get; set; }

        public string LinkValue { get; set; }

        public bool PrefixedLink { get; set; }

        public string IconMember { get; set; }

        public IconSlot IconSlot { get; set; }

        public string InvokeMethod { get; set; }

        public string InvokeTarget { get; set; }

        public Type InvokeType { get; set; }

        public IInvoker Invoker { get; set; }

        public MemberInfo MemberInfo => RubricInfo;

        public override MemberTypes MemberType => RubricInfo.MemberType;

        public override string Name => RubricInfo.Name;

        public override Type ReflectedType => RubricInfo.ReflectedType;

        public bool Required { get; set; }

        public object[] RubricAttributes
        {
            get => (VirtualInfo != null) ? VirtualInfo.RubricAttributes : null;
            set { if (VirtualInfo != null) VirtualInfo.RubricAttributes = value; }
        }

        public int RubricId { get => (int)OriginId; set => OriginId = value; }

        public int RubricOrdinal { get; set; }

        public MemberInfo RubricInfo { get; set; }

        public Module RubricModule =>
            MemberType == MemberTypes.Method ? ((MethodRubric)RubricInfo).RubricModule : null;

        public string RubricName { get; set; }

        public int RubricOffset { get; set; }

        public ParameterInfo[] RubricParameterInfo =>
            MemberType == MemberTypes.Method
                ? ((MethodRubric)RubricInfo).RubricParameterInfo
                : null;

        public Type RubricReturnType =>
            MemberType == MemberTypes.Method ? ((MethodRubric)RubricInfo).RubricReturnType : null;

        public MemberRubrics Rubrics { get; set; }

        public int RubricSize
        {
            get => VirtualInfo.RubricSize;
            set => VirtualInfo.RubricSize = value;
        }

        public Type RubricType
        {
            get => VirtualInfo.RubricType;
            set => VirtualInfo.RubricType = value;
        }

        public MethodInfo Setter =>
            MemberType == MemberTypes.Property ? ((PropertyRubric)RubricInfo).SetMethod : null;

        public AggregationOperand AggregationOperand { get; set; }

        public int AggregationOrdinal { get; set; }

        public IRubric AggregateRubric { get; set; }

        public IMemberRubric VirtualInfo => (IMemberRubric)RubricInfo;

        public SortDirection SortBy { get; set; }

        public bool Visible { get; set; }

        public string CodeNo { get => code.CodeNo; set => code.CodeNo = value; }

        public long OriginId { get => code.OriginId; set => code.OriginId = value; }

        public DateTime Created { get; set; }

        public string Creator { get; set; }

        public long Id { get => code.Id; set => code.Id = value; }

        public long TypeId { get => code.TypeId; set => code.TypeId = value; }

        public DateTime Modified { get; set; }

        public string Modifier { get; set; }

        public int Index { get => (int)code.OriginId; set => code.OriginId = (uint)value; }

        public string TypeName { get; set; }

        public DateTime Time { get => DateTime.FromBinary(code.Time); set => code.Time = value.ToBinary(); }

        public string Label { get => label ??= DisplayName ?? RubricName; set => label = value; }
    }
}
