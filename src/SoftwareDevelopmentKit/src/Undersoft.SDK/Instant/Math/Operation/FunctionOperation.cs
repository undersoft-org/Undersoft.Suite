namespace Undersoft.SDK.Instant.Math.Operation
{
    using Math.Formulas;
    using Set;
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using Undersoft.SDK.Instant.Math;
    using Unsigned.Operator;

    [Serializable]
    public class FunctionOperation : UnsignedOperator
    {
        internal FunctionType effx;

        public FunctionOperation(Formula ee, FunctionType fx) : base(ee)
        {
            effx = fx;
        }

        public enum FunctionType
        {
            Cos,
            Sin,
            Ln,
            Log
        };

        public override MathSetSize Size
        {
            get { return e.Size; }
        }

        public override void Compile(ILGenerator g, InstantMathCompilerContext cc)
        {
            if (cc.IsFirstPass())
            {
                e.Compile(g, cc);
                return;
            }
            MethodInfo mi = null;

            switch (effx)
            {
                case FunctionType.Cos:
                    mi = typeof(Math).GetMethod("Cos");
                    break;
                case FunctionType.Sin:
                    mi = typeof(Math).GetMethod("Sin");
                    break;
                case FunctionType.Ln:
                    mi = typeof(Math).GetMethod("Log");
                    break;
                case FunctionType.Log:
                    mi = typeof(Math).GetMethod("Log10");
                    break;
                default:
                    break;
            }
            if (mi == null)
                return;

            e.Compile(g, cc);

            g.EmitCall(OpCodes.Call, mi, null);
        }
    }
}
