namespace Undersoft.SDK.Instant
{
    using Rubrics.Attributes;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    public class InstantCompilerBaseConstructors
    {
        protected readonly ConstructorInfo DataMemberCtor =
            typeof(DataMemberAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly PropertyInfo[] DataMemberProps = new[]
        {
            typeof(DataMemberAttribute).GetProperty("Order"),
            typeof(DataMemberAttribute).GetProperty("Name")
        };

        protected readonly ConstructorInfo DisplayRubricCtor =
            typeof(DisplayRubricAttribute).GetConstructor(new Type[] { typeof(string) });

        protected readonly ConstructorInfo RubricSizeCtor =
            typeof(RubricSizeAttribute).GetConstructor(new Type[] { typeof(int) });

        protected readonly ConstructorInfo LinkRubricCtor = typeof(LinkAttribute).GetConstructor(
            Type.EmptyTypes
        );

        protected readonly ConstructorInfo IconRubricCtor =
            typeof(IconRubricAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly ConstructorInfo InvokeRubricCtor =
            typeof(InvokeAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly ConstructorInfo IdentityRubricCtor =
            typeof(IdentityRubricAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly ConstructorInfo KeyRubricCtor =
            typeof(KeyRubricAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly ConstructorInfo KeyCtor = typeof(KeyAttribute).GetConstructor(
            Type.EmptyTypes
        );

        protected readonly ConstructorInfo RequiredRubricCtor =
            typeof(RequiredRubricAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly ConstructorInfo DisabledRubricCtor =
            typeof(DisabledRubricAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly ConstructorInfo VisibleRubricCtor =
            typeof(VisibleRubricAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly ConstructorInfo RequiredCtor = typeof(RequiredAttribute).GetConstructor(
            Type.EmptyTypes
        );

        protected readonly ConstructorInfo AggregateRubricCtor =
            typeof(AggregateRubricAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly ConstructorInfo ExtendedRubricCtor =
            typeof(ExtendedAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly ConstructorInfo SortableRubricCtor =
            typeof(SortableAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly ConstructorInfo FilterableRubricCtor =
            typeof(FilterableAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly ConstructorInfo FileRubricCtor =
            typeof(FileRubricAttribute).GetConstructor(Type.EmptyTypes);

        protected readonly ConstructorInfo MarshalAsCtor =
            typeof(MarshalAsAttribute).GetConstructor(new Type[] { typeof(UnmanagedType) });

        protected readonly ConstructorInfo StructLayoutCtor =
            typeof(StructLayoutAttribute).GetConstructor(new Type[] { typeof(LayoutKind) });

        protected readonly FieldInfo[] StructLayoutFields = new[]
        {
            typeof(StructLayoutAttribute).GetField("CharSet"),
            typeof(StructLayoutAttribute).GetField("Pack")
        };
    }
}
