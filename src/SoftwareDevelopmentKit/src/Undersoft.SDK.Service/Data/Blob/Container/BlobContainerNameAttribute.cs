using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.Core.Internal;
using JetBrains.Annotations;
using Microsoft.IdentityModel.Tokens;

namespace Undersoft.SDK.Service.Data.Blob.Container
{
    public class BlobContainerNameAttribute : Attribute
    {
        [DisallowNull]
        public string Name { get; }

        public BlobContainerNameAttribute([DisallowNull] string name)
        {
            if (!name.IsNullOrEmpty())
            {
                Name = name;
            }
        }

        public virtual string GetName(Type type)
        {
            return Name;
        }

        public static string GetContainerName<T>()
        {
            return GetContainerName(typeof(T));
        }

        public static string GetContainerName(Type type)
        {
            var nameAttribute = type.GetCustomAttribute<BlobContainerNameAttribute>();

            if (nameAttribute == null)
            {
                return type.FullName;
            }

            return nameAttribute.GetName(type);
        }
    }
}