using System.Reflection;

namespace Undersoft.SDK.Extracting
{
    public static class PropertyInfoExtractExtenstion
    {
        public static string GetBackingFieldName(string propertyName)
        {
            return $"<{propertyName}>k__BackingField";
        }

        public static bool HaveBackingField(string propertyName)
        {
            return (GetBackingFieldName(propertyName) != null);
        }

        public static bool HaveBackingField(this PropertyInfo property)
        {
            return (GetBackingFieldName(property.Name) != null);
        }

        private static FieldInfo GetBackingField(Type type, string propertyName)
        {
            return type.GetField(
                GetBackingFieldName(propertyName),
                BindingFlags.Instance | BindingFlags.NonPublic
            );
        }

        private static FieldInfo GetBackingField(object obj, string propertyName)
        {
            return obj.GetType()
                .GetField(
                    GetBackingFieldName(propertyName),
                    BindingFlags.Instance | BindingFlags.NonPublic
                );
        }
    }
}
