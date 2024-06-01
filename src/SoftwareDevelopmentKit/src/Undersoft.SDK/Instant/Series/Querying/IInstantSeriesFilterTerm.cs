namespace Undersoft.SDK.Instant.Series.Querying
{
    public interface IInstantSeriesFilterTerm
    {
        LogicType Logic { get; set; }

        OperandType Operand { get; set; }

        string RubricName { get; set; }

        FilterStage Stage { get; set; }

        object Value { get; set; }

    }
}