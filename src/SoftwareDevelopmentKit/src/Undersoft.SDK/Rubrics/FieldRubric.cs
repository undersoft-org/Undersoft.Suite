using Undersoft.SDK.Instant;

namespace Undersoft.SDK.Rubrics
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class FieldRubric : FieldInfo, IMemberRubric
    {
        public FieldRubric() { }

        public FieldRubric(FieldInfo field, int size = 0, int fieldId = 0)
            : this(field.FieldType, field.Name, size, fieldId)
        {
            if (!(field is FieldBuilder))
            {
                if (field.GetCustomAttribute(typeof(CompilerGeneratedAttribute)) != null)
                {
                    string name = field.Name;
                    int end = name.LastIndexOf('>'),
                        start = name.IndexOf('<') + 1,
                        length = end - start;
                    RubricName = new string(field.Name.ToCharArray(start, length));
                    IsBackingField = true;
                }
            }
            RubricInfo = field;
        }

        public FieldRubric(Type fieldType, string fieldName, int size = 0, int fieldId = 0)
        {
            RubricType = fieldType;
            RubricName = fieldName;
            FieldName = fieldName;
            RubricId = fieldId;
            if (size > 0)
                RubricSize = size;
            else
            {
                if (!fieldType.IsGenericType)
                {
                    if (fieldType.IsValueType)
                    {
                        if (fieldType == typeof(DateTime))
                            RubricSize = 8;
                        else if (fieldType.IsEnum)
                            RubricSize = 4;
                        else
                            RubricSize = Marshal.SizeOf(fieldType);
                    }
                    else
                    {
                        RubricSize = Marshal.SizeOf(typeof(nint));
                    }

                    if (size > 0)
                        RubricSize = size;
                }
            }
        }

        public override FieldAttributes Attributes =>
            RubricInfo != null ? RubricInfo.Attributes : FieldAttributes.HasDefault;

        public override Type DeclaringType => RubricInfo != null ? RubricInfo.DeclaringType : null;

        public bool Editable { get; set; }

        public override RuntimeFieldHandle FieldHandle =>
            RubricInfo != null ? RubricInfo.FieldHandle : throw new NotImplementedException();

        public string FieldName { get; set; }

        public override Type FieldType => RubricType;

        public bool IsBackingField { get; set; }

        public override string Name => RubricName;

        public override Type ReflectedType => RubricInfo != null ? RubricInfo.ReflectedType : null;

        public object[] RubricAttributes { get; set; }

        public int RubricId { get; set; }

        public MemberInfo MemberInfo => RubricInfo;

        public FieldInfo RubricInfo { get; set; }

        public Module RubricModule
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string RubricName { get; set; }

        public int RubricOffset { get; set; }

        public int RubricOrdinal { get; set; }

        public PropertyInfo[] RubricParameterInfo
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Type RubricReturnType
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public int RubricSize { get; set; }

        public Type RubricType { get; set; }

        public bool Visible { get; set; }

        public string DisplayName { get; set; }

        public override object[] GetCustomAttributes(bool inherit)
        {
            if (RubricAttributes != null)
                return RubricAttributes;

            RubricAttributes = new object[0];
            if (RubricInfo != null)
            {
                var attrib = RubricInfo.GetCustomAttributes(inherit);
                if (attrib != null)
                {
                    if (RubricType.IsArray || RubricType == typeof(string))
                    {
                        if (attrib.Where(r => r is MarshalAsAttribute).Any())
                        {
                            attrib
                                .Where(r => r is MarshalAsAttribute)
                                .Cast<MarshalAsAttribute>()
                                .Select(a => RubricSize = a.SizeConst)
                                .ToArray();
                            return RubricAttributes = attrib;
                        }
                        else
                            RubricAttributes.Concat(attrib).ToArray();
                    }
                    else
                        return RubricAttributes.Concat(attrib).ToArray();
                }
            }

            if (RubricType == typeof(string))
            {
                if (RubricSize < 1)
                    RubricSize = 16;
                return new[]
                {
                    new MarshalAsAttribute(UnmanagedType.ByValTStr) { SizeConst = RubricSize }
                };
            }
            else if (RubricType.IsArray)
            {
                if (RubricSize < 1)
                    RubricSize = 8;

                if (RubricType == typeof(byte[]))
                    return RubricAttributes
                        .Concat(
                            new[]
                            {
                                new MarshalAsAttribute(UnmanagedType.ByValArray)
                                {
                                    SizeConst = RubricSize,
                                    ArraySubType = UnmanagedType.U1
                                }
                            }
                        )
                        .ToArray();
                if (RubricType == typeof(char[]))
                    return RubricAttributes
                        .Concat(
                            new[]
                            {
                                new MarshalAsAttribute(UnmanagedType.ByValArray)
                                {
                                    SizeConst = RubricSize,
                                    ArraySubType = UnmanagedType.U1
                                }
                            }
                        )
                        .ToArray();
                if (RubricType == typeof(int[]))
                    return RubricAttributes
                        .Concat(
                            new[]
                            {
                                new MarshalAsAttribute(UnmanagedType.ByValArray)
                                {
                                    SizeConst = RubricSize / 4,
                                    ArraySubType = UnmanagedType.I4
                                }
                            }
                        )
                        .ToArray();
                if (RubricType == typeof(long[]))
                    return RubricAttributes
                        .Concat(
                            new[]
                            {
                                new MarshalAsAttribute(UnmanagedType.ByValArray)
                                {
                                    SizeConst = RubricSize / 8,
                                    ArraySubType = UnmanagedType.I8
                                }
                            }
                        )
                        .ToArray();
                if (RubricType == typeof(float[]))
                    return RubricAttributes
                        .Concat(
                            new[]
                            {
                                new MarshalAsAttribute(UnmanagedType.ByValArray)
                                {
                                    SizeConst = RubricSize / 4,
                                    ArraySubType = UnmanagedType.R4
                                }
                            }
                        )
                        .ToArray();
                if (RubricType == typeof(double[]))
                    return RubricAttributes
                        .Concat(
                            new[]
                            {
                                new MarshalAsAttribute(UnmanagedType.ByValArray)
                                {
                                    SizeConst = RubricSize / 8,
                                    ArraySubType = UnmanagedType.R8
                                }
                            }
                        )
                        .ToArray();
            }
            return null;
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            var attribs = GetCustomAttributes(inherit);
            if (attribs != null)
                attribs = attribs.Where(r => r.GetType() == attributeType).ToArray();
            return attribs;
        }

        public override object GetValue(object obj)
        {
            if (RubricId < 0)
                return ((IInstant)obj)[RubricName];
            return ((IInstant)obj)[RubricId];
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            if (GetCustomAttributes(attributeType, inherit) != null)
                return true;
            return false;
        }

        public override void SetValue(
            object obj,
            object value,
            BindingFlags invokeAttr,
            Binder binder,
            CultureInfo culture
        )
        {
            if (RubricId < 0)
                ((IInstant)obj)[RubricName] = value;
            ((IInstant)obj)[RubricId] = value;
        }
    }
}
