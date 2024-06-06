namespace Undersoft.SDK
{
    public interface IFilter : IIdentifiable
    {
        LinkOperand Link { get; set; }
        string Member { get; set; }
        CompareOperand Operand { get; set; }
        object Value { get; set; }
    }
}