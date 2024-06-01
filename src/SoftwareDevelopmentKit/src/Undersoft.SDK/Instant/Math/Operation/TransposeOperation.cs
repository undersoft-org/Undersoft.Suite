namespace Undersoft.SDK.Instant.Math.Operation
{
    using Formulas;
    using Set;
    using System;
    using System.Reflection.Emit;
    using Undersoft.SDK.Instant.Math;
    using Unsigned.Operator;

    [Serializable]
    public class TransposeOperation : UnsignedOperator
    {
        public TransposeOperation(Formula e) : base(e) { }

        public override MathSetSize Size
        {
            get
            {
                MathSetSize o = e.Size;
                return new MathSetSize(o.cols, o.rows);
            }
        }

        public override void Compile(ILGenerator g, InstantMathCompilerContext cc)
        {
            if (cc.IsFirstPass())
            {
                e.Compile(g, cc);
                return;
            }

            int i1 = cc.GetIndexVariable(0);
            int i2 = cc.GetIndexVariable(1);
            cc.SetIndexVariable(1, i1);
            cc.SetIndexVariable(0, i2);
            e.Compile(g, cc);
            cc.SetIndexVariable(0, i1);
            cc.SetIndexVariable(1, i2);
        }
    }
}
