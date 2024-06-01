﻿namespace Undersoft.SDK.Service.Operation
{

    [AttributeUsage(AttributeTargets.Class)]
    public class ValidatorAttribute : Attribute
    {
        public ValidatorAttribute() { }

        public ValidatorAttribute(Type validatorType)
        {
            ValidatorType = validatorType;
            ValidatorTypeName = validatorType.FullName;
        }

        public ValidatorAttribute(string validatorTypeName)
        {
            ValidatorType = Assemblies.FindType(validatorTypeName);
            if (ValidatorType == null)
                ValidatorType = Assemblies.FindTypeByFullName(validatorTypeName);
            ValidatorTypeName = validatorTypeName;
        }

        public Type? ValidatorType { get; set; }

        public string? ValidatorTypeName { get; set; }
    }
}
