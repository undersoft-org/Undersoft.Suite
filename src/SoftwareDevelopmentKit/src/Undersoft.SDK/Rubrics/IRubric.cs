namespace Undersoft.SDK.Rubrics
{
    using Undersoft.SDK;
    using Undersoft.SDK.Invoking;
    using Undersoft.SDK.Rubrics.Attributes;

    public interface IRubric : IMemberRubric, IOrigin
    {
        short IdentityOrder { get; set; }

        bool IsAutoincrement { get; set; }

        bool IsDBNull { get; set; }

        bool Extended { get; set; }

        bool Sortable { get; set; }

        bool Filterable { get; set; }

        bool Expanded { get; set; }

        bool Disabled { get; set; }

        bool IsIdentity { get; set; }

        bool IsKey { get; set; }

        bool IsUnique { get; set; }

        bool Required { get; set; }

        bool IsLink { get; set; }

        bool IsFile { get; set; }

        FileRubricType FileType { get; set; }

        string DataMember { get; set; }

        string LinkValue { get; set; }

        bool PrefixedLink { get; set; }

        string IconMember { get; set; }

        IconSlot IconSlot { get; set; }

        string InvokeMethod { get; set; }

        string InvokeTarget { get; set; }

        Type InvokeType { get; set; }

        IInvoker Invoker { get; set; }

        AggregationOperand AggregationOperand { get; set; }

        int AggregationOrdinal { get; set; }

        IRubric AggregateRubric { get; set; }

        SortDirection SortBy { get; set; }
    }

    public enum AggregationOperand
    {
        None,
        Sum,
        Avg,
        Min,
        Max,
        Concat
    }
}
