namespace Undersoft.SDK
{
    using System.Linq.Expressions;
    using System.Text.Json;
    using Undersoft.SDK.Uniques;

    public class Filter : IFilter
    {
        public Filter() { }

        public Filter(string member, object value, CompareOperand operand = CompareOperand.None, LinkOperand link = LinkOperand.And)
        {
            Operand = operand;
            Member = member;
            Value = value;
            Link = link;
            Id = Unique.NewId;
            TypeId = member.UniqueKey();
        }

        public void SetMember(LambdaExpression expression)
        {
            Member = expression.GetMemberName();
        }

        public void SetOperand(string operand)
        {
            if (!Enum.TryParse<CompareOperand>(operand, true, out CompareOperand _operand))
                _operand = FilterCompareOperand.Parse(operand);
            Operand = _operand;
        }

        public void SetLink(string link)
        {
            Link = FilterLinkOperand.Parse(link);
        }

        public void ParseValue(object textValue, Type type)
        {
            Value = JsonSerializer.Deserialize(((JsonElement)Value).GetRawText(), type);
        }

        public long Id { get; set; }

        public long TypeId { get; set; }

        public CompareOperand Operand { get; set; }

        public string Member { get; set; }

        public object Value { get; set; }

        public LinkOperand Link { get; set; }

        public bool Added { get; set; }
    }
}
