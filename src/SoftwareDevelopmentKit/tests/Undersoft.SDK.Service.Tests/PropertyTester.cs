namespace Undersoft.SDK.Service.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class PropertyTester
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Is required.")]
        public static void CheckProperty<TContainer>(this TContainer propertyContainer, Expression<Func<TContainer, string>> property)
            where TContainer : INotifyPropertyChanged
        {
            CheckProperty(propertyContainer, property, "stringValue1", "stringValue2");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Is required.")]
        public static void CheckProperty<TContainer>(this TContainer propertyContainer, Expression<Func<TContainer, bool>> property)
            where TContainer : INotifyPropertyChanged
        {
            CheckProperty(propertyContainer, property, true, false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Is required.")]
        public static void CheckProperty<TContainer>(this TContainer propertyContainer, Expression<Func<TContainer, Guid>> property)
            where TContainer : INotifyPropertyChanged
        {
            CheckProperty(propertyContainer, property, new Guid("87B1902E-6CE3-4465-920F-2314F4196534"), new Guid("CC791033-8A6A-47A7-BC3F-ECF397D0977E"));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Is required.")]
        public static void CheckProperty<TContainer>(this TContainer propertyContainer, Expression<Func<TContainer, int>> property)
            where TContainer : INotifyPropertyChanged
        {
            CheckProperty(propertyContainer, property, 64354, 234624476);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Is required.")]
        public static void CheckProperty<TContainer>(this TContainer propertyContainer, Expression<Func<TContainer, short>> property)
            where TContainer : INotifyPropertyChanged
        {
            CheckProperty(propertyContainer, property, (short)1234, (short)3526);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Is required.")]
        public static void CheckProperty<TContainer>(this TContainer propertyContainer, Expression<Func<TContainer, byte>> property)
            where TContainer : INotifyPropertyChanged
        {
            CheckProperty(propertyContainer, property, (byte)12, (byte)53);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Is required.")]
        public static void CheckProperty<TContainer>(this TContainer propertyContainer, Expression<Func<TContainer, decimal>> property)
            where TContainer : INotifyPropertyChanged
        {
            CheckProperty(propertyContainer, property, 12.13m, 53.53m);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Is required.")]
        public static void CheckProperty<TContainer>(this TContainer propertyContainer, Expression<Func<TContainer, long>> property)
            where TContainer : INotifyPropertyChanged
        {
            CheckProperty(propertyContainer, property, 7544563756573356L, 343765427624562L);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Is required.")]
        public static void CheckProperty<TContainer>(this TContainer propertyContainer, Expression<Func<TContainer, DateTime>> property)
            where TContainer : INotifyPropertyChanged
        {
            CheckProperty(propertyContainer, property, new DateTime(2001, 1, 1), new DateTime(2001, 1, 2));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Is required.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Doesn't allow compiler to infer types.")]
        public static void CheckProperty<TContainer, TProperty>(this TContainer propertyContainer, Expression<Func<TContainer, TProperty>> property, TProperty value1, TProperty value2)
            where TContainer : INotifyPropertyChanged
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            // get the property
            if (!(property.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid property");
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid property");
            }

            // check we found the property and that it's the right type
            Assert.AreEqual(propertyInfo.PropertyType, typeof(TProperty));

            var getMethod = propertyInfo.GetMethod;
            var setMethod = propertyInfo.SetMethod;

            if (getMethod == null || setMethod == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid property that contains a getter and setter");
            }

            if (getMethod.IsPrivate || setMethod.IsPrivate)
            {
                throw new ArgumentException("Usage of private setters or getters is not allowed");
            }

            // check can get and set
            try
            {
                getMethod.Invoke(propertyContainer, new object[] { });
            }
            catch
            {
                Assert.Fail("Get method could not be invoked");
            }

            try
            {
                setMethod.Invoke(propertyContainer, new object[] { value1 });
            }
            catch
            {
                Assert.Fail("Set method could not be invoked");
            }
        }
    }
}
