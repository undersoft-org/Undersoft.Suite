namespace Undersoft.SDK.Instant.Math.Set
{
    using Formulas;
    using System.Linq.Expressions;
    using System.Reflection.Emit;
    using Undersoft.SDK.Instant.Math;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Logging;

    public class MathSet<T> : MathSet
    {
        public MathSet(MathSetFormula rubric) : base(rubric)
        {
        }

        public SubMathSet this[Expression<Func<T, object>> member]
        {
            get => this[member.GetMemberName()];
        }
    }

    [Serializable]
    public class MathSet : LeftFormula
    {
        [NonSerialized]
        private InstantMathCompilerContext context;

        public MathSet(MathSetFormula rubric)
        {
            Rubric = rubric;
            Formuler = rubric.Set;
        }

        public MathSet Formuler { get; set; }
        public SubMathSet SubFormuler { get; set; }
        public Formula Formula
        {
            get => Rubric.Formula;
            set => Rubric.Formula = value;
        }
        public Formula PartialFormula;
        public Mathstage SubFormula;

        public bool PartialMathset = false;

        public IInstantSeries Data
        {
            get => Rubric.RoutineSet.Data;
        }

        public MathSetRoutine Rubrics
        {
            get => Rubric.Routine;
            set => Rubric.Routine = value;
        }
        public MathSetFormula Rubric { get; set; }

        public string RubricName
        {
            get => Rubric.RubricName;
        }
        public Type RubricType
        {
            get => Rubric.RubricType;
        }
        public int FieldId
        {
            get => Rubric.InstantCreatorFieldId;
        }

        public int rowCount { get; set; }
        public int colCount
        {
            get => Rubrics.Count;
        }

        public int startId = 0;

        public MathSetFormula AssignRubric(string name)
        {
            return Rubric.AssignRubric(name);
        }

        public MathSetFormula AssignRubric(int ordinal)
        {
            return Rubric.AssignRubric(ordinal);
        }

        public MathSetFormula RemoveRubric(string name)
        {
            return Rubric.AssignRubric(name);
        }

        public void AssignContext(InstantMathCompilerContext Context)
        {
            if (context == null || !ReferenceEquals(context, Context))
                context = Context;
        }

        public MathSet Clone()
        {
            MathSet mx = (MathSet)MemberwiseClone();
            return mx;
        }

        public double this[long index]
        {
            get
            {
                int length = Data.GetType().GetFields().Length - 1;
                return Convert.ToDouble(Data[(int)index / length][(int)index % length]);
            }
            set
            {
                int length = Data.GetType().GetFields().Length - 1;
                Data[(int)index / length][(int)index % length] = value;
            }
        }
        public double this[long index, long field]
        {
            get { return Convert.ToDouble(Data[(int)index, (int)field]); }
            set { Data[(int)index, (int)field] = value; }
        }
        public SubMathSet this[string name]
        {
            get
            {
                if (SubFormula == null)
                    SubFormula = new Mathstage(this);
                return SubFormula[name];
            }
        }
        public SubMathSet this[int r, string name]
        {
            get
            {
                if (SubFormula == null)
                    SubFormula = new Mathstage(this, r, r);
                return SubFormula[name];
            }
        }
        public Mathstage this[int r]
        {
            get { return new Mathstage(this, r, r); }
        }
        public Mathstage this[IndexRange q]
        {
            get { return new Mathstage(this, q.first, q.last); }
        }

        public static IndexRange Range(int i1, int i2)
        {
            return new IndexRange(i1, i2);
        }

        public override void CompileAssign(
            ILGenerator g,
            InstantMathCompilerContext cc,
            bool post,
            bool partial
        )
        {
            if (cc.IsFirstPass())
            {
                cc.Add(Data);
                PartialFormula = Formula.RightFormula.Prepare(this[RubricName], false);
                PartialFormula.Compile(g, cc);
                Rubric.PartialMathset = true;
            }
            else
            {
                PartialFormula.Compile(g, cc);
            }
        }

        public override void Compile(ILGenerator g, InstantMathCompilerContext cc)
        {
            if (cc.IsFirstPass())
            {
                cc.Add(Data);
                PartialFormula = Formula.RightFormula.Prepare(this[RubricName], true);
                PartialFormula.Compile(g, cc);
                Rubric.PartialMathset = true;
            }
            else
            {
                PartialFormula.Compile(g, cc);
            }
        }

        public override MathSetSize Size
        {
            get { return new MathSetSize(Data.Count, Rubrics.Count); }
        }

        public void SetDimensions(SubMathSet sm, MathSet mx = null, int offset = 0)
        {
            sm.startId = offset;
            sm.SetDimensions(mx);
        }

        public SubMathSet GetAll(MathSetFormula rubric)
        {
            SubMathSet smx = new SubMathSet(rubric, this);
            return smx;
        }

        public SubMathSet GetRange(int startRowId, int endRowId, MathSetFormula rubric)
        {
            startId = startRowId;
            rowCount = endRowId - startRowId + 1;
            SubMathSet smx = new SubMathSet(rubric, this);
            return smx;
        }

        public SubMathSet GetColumn(int j)
        {
            return GetRange(0, j, null);
        }

        public SubMathSet GetColumnCount(int j1, int j2)
        {
            return GetRange(0, 1, null);
        }

        public SubMathSet GetRow(int i)
        {
            return GetRange(i, 1, null);
        }

        public SubMathSet GetRowCount(int i1, int i2)
        {
            return GetRange(i1, i2, null);
        }

        public SubMathSet GetItems(int e1, int e2)
        {
            return new SubMathSet(null, this);
        }

        [Serializable]
        public class Mathstage
        {
            internal Mathstage(MathSet m)
            {
                formuler = m;
                firstRow = 0;
                rowCount = m.rowCount - firstRow - 1;
            }

            internal Mathstage(MathSet m, int startRowId, int endRowId)
            {
                firstRow = startRowId;
                rowCount = endRowId - startRowId;
                formuler = m;
            }

            public SubMathSet this[int ordinal]
            {
                get
                {
                    MathSetFormula rubric = formuler.Rubric.AssignRubric(ordinal);
                    return formuler.GetAll(rubric);
                }
            }
            public SubMathSet this[string name]
            {
                get
                {
                    try
                    {
                        MathSetFormula rubric = formuler.Rubric.AssignRubric(name);
                        return formuler.GetAll(rubric);
                    }
                    catch (Exception ex)
                    {
                        this.Warning<Instantlog>("Assign rubric by name to formula failed", null, ex);
                        return null;
                    }
                }
            }

            public static explicit operator LeftFormula(Mathstage r)
            {
                return r.formuler.GetItems(r.firstRow, r.lastRow);
            }

            private MathSet formuler;

            public int firstRow;
            public int rowCount = -1;
            public int lastRow
            {
                get
                {
                    return formuler.rowCount > firstRow + rowCount + 1 && rowCount > -1
                        ? firstRow + rowCount
                        : formuler.rowCount - 1;
                }
            }
        }

        [Serializable]
        public struct IndexRange
        {
            internal IndexRange(int i1, int i2)
            {
                first = i1;
                last = i2;
            }

            internal int first,
                last;
        }
    }
}
