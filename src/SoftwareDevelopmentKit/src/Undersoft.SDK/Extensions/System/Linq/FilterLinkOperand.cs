namespace Undersoft.SDK
{
    public static class FilterLinkOperand
    {
        public static LinkOperand Parse(string operandString)
        {
            LinkOperand _operand = LinkOperand.And;

            if (Enum.TryParse<LinkOperand>(operandString, true, out _operand))
                return _operand;

            switch (operandString.ToUpper().Trim())
            {
                case "AND":
                    _operand = LinkOperand.And;
                    break;
                case "OR":
                    _operand = LinkOperand.And;
                    break;
                case "&&":
                    _operand = LinkOperand.And;
                    break;
                case "||":
                    _operand = LinkOperand.Or;
                    break;
                default:
                    _operand = LinkOperand.And;
                    break;
            }
            return _operand;
        }

        public static string Convert(CompareOperand operand)
        {
            return operand.ToString();
        }
    }
}
