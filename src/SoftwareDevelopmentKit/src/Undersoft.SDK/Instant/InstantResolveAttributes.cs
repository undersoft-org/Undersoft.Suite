namespace Undersoft.SDK.Instant
{
    using Rubrics.Attributes;
    using System.ComponentModel.DataAnnotations;
    using Undersoft.SDK.Invoking;
    using Undersoft.SDK.Series;

    public static class InstantResolveAttributes
    {
        public static ISeries<IInvoker> Registry;
        private static InstantCompilerBase InstantCompilerBase;

        static InstantResolveAttributes()
        {
            Registry = new Registry<IInvoker>();
            InstantCompilerBase = new InstantCompilerBase();

            Registry.Add(
                typeof(KeyAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorKeyAttributes
                )
            );
            Registry.Add(
                typeof(KeyRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorKeyAttributes
                )
            );
            Registry.Add(
                typeof(IdentityRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorIdentityAttributes
                )
            );
            Registry.Add(
                typeof(RequiredRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorRquiredAttributes
                )
            );
            Registry.Add(
                typeof(RequiredAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorRquiredAttributes
                )
            );
            Registry.Add(
                typeof(VisibleRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorVisibleAttributes
                )
            );
            Registry.Add(
                typeof(DisplayRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorDisplayAttributes
                )
            );
            Registry.Add(
                typeof(AggregateRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorAggregateAttributes
                )
            );
            Registry.Add(
                typeof(ExtendedAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorExtendedAttributes
                )
            );
            Registry.Add(
                typeof(FileRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorFileAttributes
                )
            );
            Registry.Add(
                typeof(LinkAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorLinkAttributes
                )
            );
            Registry.Add(
                typeof(InvokeAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorInvokeAttributes
                )
            );
            Registry.Add(
                typeof(IconRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorIconAttributes
                )
            );
            Registry.Add(
                typeof(DisabledRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorDisabledAttributes
                )
            );
            Registry.Add(
                typeof(RubricSizeAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorSizeAttributes
                )
            );
            Registry.Add(
                typeof(SortableAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorSortableAttributes
                )
            );
            Registry.Add(
                typeof(FilterableAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantCreatorFilterableAttributes
                )
            );
        }
    }
}
