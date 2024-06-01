namespace Undersoft.SDK.Instant.Math.Operation.Binary.Formulas
{
    using Math.Formulas;
    using System;

    [Serializable]
    public abstract class BinaryFormula : Formula
    {
        protected Formula expr1,
            expr2;

        public BinaryFormula(Formula e1, Formula e2)
        {
            expr1 = e1;
            expr2 = e2;
        }
    }
}
