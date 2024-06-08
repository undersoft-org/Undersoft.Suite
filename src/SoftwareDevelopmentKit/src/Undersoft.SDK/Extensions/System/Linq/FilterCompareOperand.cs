namespace Undersoft.SDK
{
    public static class FilterCompareOperand
    {
        public static CompareOperand Parse(string operandString)
        {
            CompareOperand _operand = CompareOperand.None;
            switch (operandString)
            {
                case "=":
                    _operand = CompareOperand.Equal;
                    break;
                case ">=":
                    _operand = CompareOperand.GreaterOrEqual;
                    break;
                case ">":
                    _operand = CompareOperand.Greater;
                    break;
                case "<=":
                    _operand = CompareOperand.LessOrEqual;
                    break;
                case "<":
                    _operand = CompareOperand.Less;
                    break;
                case "*":
                    _operand = CompareOperand.Contains;
                    break;
                case "!*":
                    _operand = CompareOperand.NotContains;
                    break;
                default:
                    _operand = CompareOperand.Equal;
                    break;
            }
            return _operand;
        }

        public static string Convert(CompareOperand operand)
        {
            string operandString = "";
            switch (operand)
            {
                case CompareOperand.Equal:
                    operandString = "=";
                    break;
                case CompareOperand.GreaterOrEqual:
                    operandString = ">=";
                    break;
                case CompareOperand.Greater:
                    operandString = ">";
                    break;
                case CompareOperand.LessOrEqual:
                    operandString = "<=";
                    break;
                case CompareOperand.Less:
                    operandString = "<";
                    break;
                case CompareOperand.Contains:
                    operandString = "*";
                    break;
                case CompareOperand.NotContains:
                    operandString = "!*";
                    break;
                case CompareOperand.StartsWith:
                    operandString = "<*";
                    break;
                case CompareOperand.EndsWith:
                    operandString = "*>";
                    break;
                default:
                    operandString = "=";
                    break;
            }
            return operandString;
        }
    }
}
