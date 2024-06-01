namespace Undersoft.SDK.Rubrics.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class AggregateRubricAttribute : RubricAttribute
    {
        public AggregationOperand Operand = AggregationOperand.None;

        public AggregateRubricAttribute() { }
    }
}
