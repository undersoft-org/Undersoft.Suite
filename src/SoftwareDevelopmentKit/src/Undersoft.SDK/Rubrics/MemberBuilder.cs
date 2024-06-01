namespace Undersoft.SDK.Rubrics
{
    using System;
    using System.Reflection;
    using Undersoft.SDK.Uniques;

    public class MemberBuilder : Identifiable
    {
        public MemberBuilder(MemberRubric member) : base()
        {
            SetMember(member);
            Id = Name.UniqueKey();
        }

        public MemberInfo SetMember(MemberRubric member)
        {
            if (member.MemberType == MemberTypes.Field)
            {
                SetField((FieldRubric)member.RubricInfo);
                Member ??= member;
                Member.InstantField = member.InstantField;
            }
            else if (member.MemberType == MemberTypes.Property)
            {
                SetProperty((PropertyRubric)member.RubricInfo);
                Member = member;
            }
            return null;
        }

        public PropertyRubric SetProperty(PropertyRubric property)
        {
            Property = property;
            Name ??= property.RubricName;
            Type = property.PropertyType;
            Getter = property.GetGetMethod();
            Setter = property.GetSetMethod();
            return property;
        }

        public FieldRubric SetField(FieldRubric field)
        {
            Field = field;
            Type ??= field.FieldType;
            FieldType = field.FieldType;
            FieldName = field.FieldName;
            Name ??= field.RubricName;
            return field;
        }

        public string Name { get; set; }
        public string FieldName { get; set; }
        public Type Type { get; set; }
        public MemberRubric Member { get; set; }
        public PropertyRubric Property { get; set; }
        public FieldRubric Field { get; set; }
        public MethodInfo Getter { get; set; }
        public MethodInfo Setter { get; set; }
        public Type FieldType { get; set; }
    }
}
