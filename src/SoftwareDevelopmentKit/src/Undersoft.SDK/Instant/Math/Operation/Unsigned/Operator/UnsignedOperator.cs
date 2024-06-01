namespace Undersoft.SDK.Instant.Math.Operation.Unsigned.Operator
{
    using Math.Formulas;
    using Set;
    using System;
    using System.Reflection.Emit;
    using Undersoft.SDK.Instant.Math;

    [Serializable]
    public class UnsignedOperator : Formula
    {
        protected Formula e;

        public UnsignedOperator(Formula ee)
        {
            e = ee;
        }

        public override MathSetSize Size
        {
            get { return e.Size; }
        }

        public override void Compile(ILGenerator g, InstantMathCompilerContext cc)
        {
            e.Compile(g, cc);
        }
    }
}
