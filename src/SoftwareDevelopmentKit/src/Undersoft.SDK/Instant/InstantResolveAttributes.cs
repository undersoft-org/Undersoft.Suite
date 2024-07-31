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
                    m => m.ResolveInstantGeneratorKeyAttributes
                )
            );
            Registry.Add(
                typeof(KeyRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorKeyAttributes
                )
            );
            Registry.Add(
                typeof(IdentityRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorIdentityAttributes
                )
            );
            Registry.Add(
                typeof(RequiredRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorRquiredAttributes
                )
            );
            Registry.Add(
                typeof(RequiredAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorRquiredAttributes
                )
            );
            Registry.Add(
                typeof(VisibleRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorVisibleAttributes
                )
            );
            Registry.Add(
                typeof(DisplayRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorDisplayAttributes
                )
            );
            Registry.Add(
                typeof(AggregateRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorAggregateAttributes
                )
            );
            Registry.Add(
                typeof(ExtendedAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorExtendedAttributes
                )
            );
            Registry.Add(
                typeof(FileRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorFileAttributes
                )
            );
            Registry.Add(
                typeof(LinkAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorLinkAttributes
                )
            );
            Registry.Add(
                typeof(InvokeAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorInvokeAttributes
                )
            );
            Registry.Add(
                typeof(IconRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorIconAttributes
                )
            );
            Registry.Add(
                typeof(DisabledRubricAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorDisabledAttributes
                )
            );
            Registry.Add(
                typeof(RubricSizeAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorSizeAttributes
                )
            );
            Registry.Add(
                typeof(SortableAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorSortableAttributes
                )
            );
            Registry.Add(
                typeof(FilterableAttribute),
                new Invoker<InstantCompilerBase>(
                    InstantCompilerBase,
                    m => m.ResolveInstantGeneratorFilterableAttributes
                )
            );
        }
    }
}
