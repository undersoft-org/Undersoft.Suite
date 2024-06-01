namespace Undersoft.SDK.Instant.Math.Operation.Binary.Operator
{
    using System;
    using System.Reflection.Emit;

    [Serializable]
    public class Divide : BinaryOperator
    {
        public override double Apply(double a, double b)
        {
            return a / b;
        }

        public override void Compile(ILGenerator g)
        {
            g.Emit(OpCodes.Div);
        }
    }
}
