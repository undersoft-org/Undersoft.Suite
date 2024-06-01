namespace Undersoft.SDK.Instant.Math.Operation
{
    using Binary.Formulas;
    using Formulas;
    using Set;
    using System;
    using System.Reflection.Emit;
    using Undersoft.SDK.Instant.Math;

    [Serializable]
    public class PowerOperation : BinaryFormula
    {
        public PowerOperation(Formula e1, Formula e2) : base(e1, e2) { }

        public override MathSetSize Size
        {
            get { return expr1.Size; }
        }

        public override void Compile(ILGenerator g, InstantMathCompilerContext cc)
        {
            if (cc.IsFirstPass())
            {
                expr1.Compile(g, cc);
                expr2.Compile(g, cc);
                if (!(expr2.Size == MathSetSize.Scalar))
                    throw new SizeMismatchException(
                        "Pow Operator requires a scalar second operand"
                    );
                return;
            }
            expr1.Compile(g, cc);
            expr2.Compile(g, cc);
            g.EmitCall(OpCodes.Call, typeof(Math).GetMethod("Pow"), null);
        }
    }
}
