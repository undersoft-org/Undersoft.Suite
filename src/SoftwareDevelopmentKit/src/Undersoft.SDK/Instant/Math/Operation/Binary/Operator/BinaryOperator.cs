namespace Undersoft.SDK.Instant.Math.Operation.Binary.Operator
{
    using System;
    using System.Reflection.Emit;

    [Serializable]
    public abstract class BinaryOperator
    {
        public abstract double Apply(double a, double b);

        public abstract void Compile(ILGenerator g);
    }
}
