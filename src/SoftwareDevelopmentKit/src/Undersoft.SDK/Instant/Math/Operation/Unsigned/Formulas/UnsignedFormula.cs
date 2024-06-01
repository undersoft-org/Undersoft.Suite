namespace Undersoft.SDK.Instant.Math.Operation.Unsigned.Formulas
{
    using Math.Formulas;
    using Set;
    using System;
    using System.Reflection.Emit;
    using Undersoft.SDK.Instant.Math;

    [Serializable]
    public class UnsignedFormula : Formula
    {
        internal double thevalue;

        public UnsignedFormula(double vv)
        {
            thevalue = vv;
        }

        public override MathSetSize Size
        {
            get { return MathSetSize.Scalar; }
        }

        public override void Compile(ILGenerator g, InstantMathCompilerContext cc)
        {
            if (cc.IsFirstPass())
                return;
            g.Emit(OpCodes.Ldc_R8, thevalue);
        }
    }
}
