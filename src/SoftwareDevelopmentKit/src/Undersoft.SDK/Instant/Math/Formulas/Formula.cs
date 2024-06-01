namespace Undersoft.SDK.Instant.Math.Formulas
{
    using Set;
    using Operation;
    using System.Reflection.Emit;
    using Operation.Binary.Operator;
    using Operation.Unsigned.Formulas;
    using Undersoft.SDK.Instant.Math;

    [Serializable]
    public abstract class Formula
    {
        [NonSerialized]
        public CombinedFormula CombinedFormula;

        [NonSerialized]
        public Formula LeftFormula;

        [NonSerialized]
        public Formula RightFormula;

        public virtual MathSetSize Size
        {
            get { return new MathSetSize(0, 0); }
        }

        public static Formula Cos(Formula e)
        {
            return new FunctionOperation(e, FunctionOperation.FunctionType.Cos);
        }

        public static Formula Log(Formula e)
        {
            return new FunctionOperation(e, FunctionOperation.FunctionType.Log);
        }

        public static Formula MemPow(Formula e1, Formula e2)
        {
            return new PowerOperation(e1, e2);
        }

        public static Formula Sin(Formula e)
        {
            return new FunctionOperation(e, FunctionOperation.FunctionType.Sin);
        }

        public MathSetComputer CompileMathSet(CombinedFormula m)
        {
            return InstantMathCompiler.Compile(m);
        }

        public MathSetComputer CompileMathSet(Formula f, LeftFormula m)
        {
            MathSetComputer mathline = InstantMathCompiler.Compile(new CombinedFormula(m, f));
            return mathline;
        }

        public abstract void Compile(ILGenerator g, InstantMathCompilerContext cc);

        public void Compute(MathSetComputer cm)
        {
            Evaluator e = new Evaluator(cm.Compute);
            e();
        }

        public Evaluator CreateEvaluator(CombinedFormula m)
        {
            MathSetComputer mathline = CompileMathSet(m);
            Evaluator ev = new Evaluator(mathline.Compute);
            return ev;
        }

        public Evaluator CreateEvaluator(MathSetComputer e)
        {
            Evaluator ev = new Evaluator(e.Compute);
            return ev;
        }

        public Evaluator CreateEvaluator(Formula f, LeftFormula m)
        {
            MathSetComputer mathline = CompileMathSet(f, m);
            Evaluator ev = new Evaluator(mathline.Compute);
            return ev;
        }

        public override bool Equals(object o)
        {
            if (o == null)
                return false;
            return Equals(o);
        }

        public override int GetHashCode()
        {
            return GetHashCode();
        }

        public Formula Pow(Formula e2)
        {
            return new PowerOperation(this, e2);
        }

        public CombinedFormula Prepare(Formula f, LeftFormula m, bool partial = false)
        {
            CombinedFormula = new CombinedFormula(m, f, partial);
            CombinedFormula.LeftFormula = m;
            CombinedFormula.RightFormula = f;
            return CombinedFormula;
        }

        public CombinedFormula Prepare(LeftFormula m, bool partial = false)
        {
            CombinedFormula = new CombinedFormula(m, this, partial);
            CombinedFormula.LeftFormula = m;
            CombinedFormula.RightFormula = this;
            return CombinedFormula;
        }

        public Formula Transpose()
        {
            return new TransposeOperation(this);
        }

        public static Formula operator +(Formula e1, Formula e2)
        {
            return new BinaryOperation(e1, e2, new Plus());
        }

        public static Formula operator -(Formula e1, Formula e2)
        {
            return new BinaryOperation(e1, e2, new Minus());
        }

        public static Formula operator *(Formula e1, Formula e2)
        {
            return new BinaryOperation(e1, e2, new Multiply());
        }

        public static Formula operator /(Formula e1, Formula e2)
        {
            return new BinaryOperation(e1, e2, new Divide());
        }

        public static Formula operator ==(Formula e1, Formula e2)
        {
            return new CompareOperation(e1, e2, new Equal());
        }

        public static Formula operator !=(Formula e1, Formula e2)
        {
            return new CompareOperation(e1, e2, new NotEqual());
        }

        public static Formula operator <(Formula e1, Formula e2)
        {
            return new CompareOperation(e1, e2, new Less());
        }

        public static Formula operator |(Formula e1, Formula e2)
        {
            return new CompareOperation(e1, e2, new OrOperand());
        }

        public static Formula operator >(Formula e1, Formula e2)
        {
            return new CompareOperation(e1, e2, new Greater());
        }

        public static Formula operator &(Formula e1, Formula e2)
        {
            return new CompareOperation(e1, e2, new AndOperand());
        }

        public static bool operator !=(Formula e1, object o)
        {
            if (o == null)
                return NullCheck.IsNotNull(e1);
            else
                return !e1.Equals(o);
        }

        public static bool operator ==(Formula e1, object o)
        {
            if (o == null)
                return NullCheck.IsNull(e1);
            else
                return e1.Equals(o);
        }

        public static implicit operator Formula(double f)
        {
            return new UnsignedFormula(f);
        }
    }

    public static class NullCheck
    {
        public static bool IsNotNull(object o)
        {
            if (o is ValueType)
                return false;
            else
                return !ReferenceEquals(o, null);
        }

        public static bool IsNull(object o)
        {
            if (o is ValueType)
                return false;
            else
                return ReferenceEquals(o, null);
        }
    }

    public class SizeMismatchException : Exception
    {
        public SizeMismatchException(string s) : base(s) { }
    }
}
