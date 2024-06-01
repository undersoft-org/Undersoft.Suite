namespace Undersoft.SDK.Rubrics
{
    using System;
    using System.Globalization;
    using System.Reflection;

    public class MethodRubric : MethodInfo, IMemberRubric
    {
        public MethodRubric() { }

        public MethodRubric(MethodInfo method, int propertyId = 0)
            : this(
                method.DeclaringType,
                method.Name,
                method.ReturnType,
                method.GetParameters(),
                method.Module,
                propertyId
            )
        {
            RubricInfo = method;
            RubricType = method.DeclaringType;
            RubricName = method.Name;
            RubricParameterInfo = method.GetParameters();
            RubricReturnType = method.ReturnType;
            RubricModule = method.Module;
        }

        public MethodRubric(
            Type declaringType,
            string propertyName,
            Type returnType,
            ParameterInfo[] parameterTypes,
            Module module,
            int propertyId = 0
        )
        {
            RubricType = declaringType;
            RubricName = propertyName;
            RubricId = propertyId;
            RubricParameterInfo = parameterTypes;
            RubricReturnType = returnType;
            RubricModule = module;
        }

        public override MethodAttributes Attributes => RubricInfo.Attributes;

        public override Type DeclaringType => RubricInfo != null ? RubricInfo.DeclaringType : null;

        public bool Editable { get; set; }

        public override RuntimeMethodHandle MethodHandle => RubricInfo.MethodHandle;

        public override string Name => RubricName;

        public override Type ReflectedType => RubricInfo != null ? RubricInfo.ReflectedType : null;

        public override ICustomAttributeProvider ReturnTypeCustomAttributes =>
            RubricInfo.ReturnTypeCustomAttributes;

        public object[] RubricAttributes { get; set; } = null;

        public int RubricId { get; set; }

        public MemberInfo MemberInfo => RubricInfo;

        public MethodInfo RubricInfo { get; set; }

        public Module RubricModule { get; set; }

        public string RubricName { get; set; }

        public int RubricOrdinal { get; set; }

        public int RubricOffset { get; set; } = 0;

        public ParameterInfo[] RubricParameterInfo { get; set; }

        public Type RubricReturnType { get; set; }

        public int RubricSize { get; set; } = 0;

        public Type RubricType { get; set; }

        public bool Visible { get; set; }

        public string DisplayName { get; set; }

        public override MethodInfo GetBaseDefinition()
        {
            return RubricInfo.GetBaseDefinition();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return RubricInfo.GetCustomAttributes(inherit);
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return RubricInfo.GetCustomAttributes(attributeType, inherit);
        }

        public override MethodImplAttributes GetMethodImplementationFlags()
        {
            return RubricInfo.GetMethodImplementationFlags();
        }

        public override ParameterInfo[] GetParameters()
        {
            return RubricInfo.GetParameters();
        }

        public override object Invoke(
            object obj,
            BindingFlags invokeAttr,
            Binder binder,
            object[] parameters,
            CultureInfo culture
        )
        {
            return RubricInfo.Invoke(obj, invokeAttr, binder, parameters, culture);
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            if (GetCustomAttributes(attributeType, inherit) != null)
                return true;
            return false;
        }
    }
}
