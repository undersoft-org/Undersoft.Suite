using Undersoft.SDK.Instant;

namespace Undersoft.SDK.Rubrics
{
    using Instant.Attributes;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class PropertyRubric : PropertyInfo, IMemberRubric
    {
        public PropertyRubric() { }

        public PropertyRubric(PropertyInfo property, int size = 0, int propertyId = 0)
            : this(property.PropertyType, property.Name, propertyId)
        {
            RubricInfo = property;
        }

        public PropertyRubric(
            Type propertyType,
            string propertyName,
            int size = 0,
            int propertyId = 0
        )
        {
            RubricType = propertyType;
            RubricName = propertyName;
            RubricId = propertyId;
            if (!propertyType.IsGenericType)
            {
                if (propertyType.IsValueType)
                {
                    if (propertyType == typeof(DateTime))
                        RubricSize = 8;
                    else if (propertyType.IsEnum)
                        RubricSize = 4;
                    else
                        RubricSize = Marshal.SizeOf(propertyType);
                }

                if (size > 0)
                    RubricSize = size;
            }
        }

        public override PropertyAttributes Attributes =>
            RubricInfo != null ? RubricInfo.Attributes : PropertyAttributes.HasDefault;

        public override bool CanRead => Visible;

        public override bool CanWrite => Editable;

        public override Type DeclaringType => RubricInfo != null ? RubricInfo.DeclaringType : null;

        public bool Editable { get; set; }

        public override string Name => RubricName;

        public override Type PropertyType => RubricType;

        public override Type ReflectedType => RubricInfo != null ? RubricInfo.ReflectedType : null;

        public object[] RubricAttributes { get; set; } = null;

        public int RubricId { get; set; } = 0;

        public MemberInfo MemberInfo => RubricInfo;

        public PropertyInfo RubricInfo { get; set; }

        public Module RubricModule
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string RubricName { get; set; }

        public int RubricOrdinal { get; set; }

        public int RubricOffset { get; set; } = 0;

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

        public int RubricSize { get; set; } = 0;

        public Type RubricType { get; set; }

        public bool Visible { get; set; }

        public string DisplayName { get; set; }

        public override MethodInfo[] GetAccessors(bool nonPublic)
        {
            if (RubricInfo != null)
                return RubricInfo.GetAccessors(nonPublic);
            return null;
        }

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
                        var _attrib = attrib.Where(r => r is InstantAsAttribute).ToArray();
                        if (_attrib.Any())
                        {
                            _attrib
                                .Cast<InstantAsAttribute>()
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
                var _attrib = RubricAttributes.Where(r => r is MarshalAsAttribute).ToArray();
                if (_attrib.Any())
                {
                    _attrib
                        .Cast<MarshalAsAttribute>()
                        .Select(a => RubricSize = a.SizeConst)
                        .ToArray();
                    return RubricAttributes;
                }

                return new[]
                {
                    new MarshalAsAttribute(UnmanagedType.ByValTStr) { SizeConst = RubricSize }
                };
            }
            else if (RubricType.IsArray)
            {
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
            {
                attribs = attribs.Where(r => r.GetType() == attributeType).ToArray();
                if (!attribs.Any())
                    return null;
            }
            return attribs;
        }

        public override MethodInfo GetGetMethod(bool nonPublic)
        {
            if (RubricInfo != null)
                return RubricInfo.GetGetMethod(nonPublic);
            return null;
        }

        public override ParameterInfo[] GetIndexParameters()
        {
            if (RubricInfo != null)
                return RubricInfo.GetIndexParameters();
            return null;
        }

        public override MethodInfo GetSetMethod(bool nonPublic)
        {
            if (RubricInfo != null)
                return RubricInfo.GetSetMethod(nonPublic);
            return null;
        }

        public override object GetValue(
            object obj,
            BindingFlags invokeAttr,
            Binder binder,
            object[] index,
            CultureInfo culture
        )
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
            object[] index,
            CultureInfo culture
        )
        {
            if (RubricId < 0)
                ((IInstant)obj)[RubricName] = value;
            ((IInstant)obj)[RubricId] = value;
        }
    }
}
