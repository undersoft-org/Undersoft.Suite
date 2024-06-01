namespace Undersoft.SDK.Instant.Math.Formulas
{
    using System;
    using System.Reflection.Emit;
    using Undersoft.SDK.Instant.Math;

    [Serializable]
    public abstract class LeftFormula : Formula
    {
        public abstract void CompileAssign(
            ILGenerator g,
            InstantMathCompilerContext cc,
            bool post,
            bool partial
        );
    }
}
