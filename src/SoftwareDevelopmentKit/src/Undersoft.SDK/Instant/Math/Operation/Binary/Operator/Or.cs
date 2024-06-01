namespace Undersoft.SDK.Instant.Math.Operation.Binary.Operator
{
    using System;
    using System.Reflection.Emit;

    [Serializable]
    public class OrOperand : BinaryOperator
    {
        public override double Apply(double a, double b)
        {
            return Convert.ToDouble(Convert.ToBoolean(a) || Convert.ToBoolean(b));
        }

        public override void Compile(ILGenerator g)
        {
            g.Emit(OpCodes.Or);
        }
    }
}
