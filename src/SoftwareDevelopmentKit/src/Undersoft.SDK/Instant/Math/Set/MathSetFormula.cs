namespace Undersoft.SDK.Instant.Math.Set
{
    using Formulas;
    using Rubrics;
    using SDK.Uniques;
    using System;
    using Undersoft.SDK.Instant.Math;

    public class MathSetFormula : IIdentifiable
    {
        [NonSerialized]
        private CombinedFormula formula;

        [NonSerialized]
        private MathSetRoutine routine;

        [NonSerialized]
        private MathSet set;

        [NonSerialized]
        private MathSetRoutine routineSet;

        [NonSerialized]
        private MemberRubric memberRubric;

        [NonSerialized]
        private MathSetComputer computer;

        public MathSetFormula(MathSetRoutine routineSet, MemberRubric rubric)
        {
            memberRubric = rubric;
            this.routineSet = routineSet;
        }

        public int ComputeOrdinal { get; set; }

        public IUnique Empty => Uscn.Empty;

        public MathSetComputer Evaluator
        {
            get { return computer; }
            set { computer = value; }
        }

        public int InstantCreatorFieldId
        {
            get => memberRubric.FieldId;
        }

        public Formula Formula
        {
            get { return !ReferenceEquals(formula, null) ? formula : null; }
            set
            {
                if (!ReferenceEquals(value, null))
                {
                    formula = value.Prepare(Set[memberRubric.RubricName]);
                }
            }
        }

        public MathSetFormula FormulaSet
        {
            get { return this; }
        }

        public MathSetRoutine Routine
        {
            get { return routine; }
            set { routine = value; }
        }

        public MathSet Set
        {
            get { return set; }
            set { set = value; }
        }

        public MathSetRoutine RoutineSet
        {
            get { return routineSet; }
            set { routineSet = value; }
        }

        public bool PartialMathset { get; set; }

        public Formula RightFormula { get; set; }

        public string RubricName
        {
            get => memberRubric.RubricName;
        }

        public Type RubricType
        {
            get => memberRubric.RubricType;
        }

        public SubMathSet SubSet { get; set; }

        public long Id
        {
            get => memberRubric.Id;
            set => memberRubric.Id = value;
        }

        public long TypeId
        {
            get => memberRubric.TypeId;
            set => memberRubric.TypeId = value;
        }

        public MathSetFormula AssignRubric(int ordinal)
        {
            if (Routine == null)
                Routine = new MathSetRoutine(routineSet);

            MathSetFormula erubric = null;
            MemberRubric rubric = RoutineSet.Rubrics[ordinal];
            if (rubric != null)
            {
                erubric = new MathSetFormula(RoutineSet, rubric);
                assignRubric(erubric);
            }
            return erubric;
        }

        public MathSetFormula AssignRubric(string name)
        {
            if (Routine == null)
                Routine = new MathSetRoutine(routineSet);

            MathSetFormula erubric = null;
            MemberRubric rubric = RoutineSet.Rubrics[name];
            if (rubric != null)
            {
                erubric = new MathSetFormula(RoutineSet, rubric);
                assignRubric(erubric);
            }
            return erubric;
        }

        public MathSet CloneMathset()
        {
            return set.Clone();
        }

        public MathSetComputer CombineEvaluator()
        {
            if (computer == null)
                computer = GetCompiledMathSet();

            return computer;
        }

        public MathSetComputer GetCompiledMathSet()
        {
            return formula.CompileMathSet(formula);
        }

        public int CompareTo(IUnique other)
        {
            return (int)(Id - other.Id);
        }

        public LeftFormula Compute()
        {
            if (computer != null)
            {
                Evaluator reckon = new Evaluator(computer.Compute);
                reckon();
            }
            return formula.lexpr;
        }

        public bool Equals(IUnique other)
        {
            return Id == other.Id;
        }

        public byte[] GetBytes()
        {
            return GetBytes();
        }

        public MathSet<T> GetMathset<T>()
        {
            if (!ReferenceEquals(Set, null))
                return (MathSet<T>)Set;
            else
            {
                routine = new MathSetRoutine(routineSet);
                var mathSet = new MathSet<T>(this);
                Set = mathSet;
                return mathSet;
            }
        }

        public MathSet GetMathset()
        {
            if (!ReferenceEquals(Set, null))
                return Set;
            else
            {
                routine = new MathSetRoutine(routineSet);
                return Set = new MathSet(this);
            }
        }

        public byte[] GetIdBytes()
        {
            return Id.UniqueBytes64();
        }

        public MathSet NewMathset()
        {
            return Set = new MathSet(this);
        }

        public MathSetFormula RemoveRubric(int ordinal)
        {
            MathSetFormula erubric = null;
            MemberRubric rubric = RoutineSet.Rubrics[ordinal];
            if (rubric != null)
            {
                erubric = RoutineSet[rubric];
                removeRubric(erubric);
            }
            return erubric;
        }

        public MathSetFormula RemoveRubric(string name)
        {
            MathSetFormula erubric = null;
            MemberRubric rubric = RoutineSet.Rubrics[name];
            if (rubric != null)
            {
                erubric = RoutineSet[rubric];
                removeRubric(erubric);
            }
            return erubric;
        }

        private MathSetFormula assignRubric(MathSetFormula erubric)
        {
            if (!Routine.Contains(erubric))
            {
                if (!RoutineSet.RoutineSet.Contains(erubric))
                {
                    RoutineSet.RoutineSet.Add(erubric);
                }

                if (
                    erubric.InstantCreatorFieldId == FormulaSet.InstantCreatorFieldId
                    && !RoutineSet.FormulaSet.Contains(erubric)
                )
                    RoutineSet.FormulaSet.Add(erubric);

                Routine.Add(erubric);
            }
            return erubric;
        }

        private MathSetFormula removeRubric(MathSetFormula erubric)
        {
            if (!Routine.Contains(erubric))
            {
                Routine.Remove(erubric);

                if (!RoutineSet.RoutineSet.Contains(erubric))
                    RoutineSet.RoutineSet.Remove(erubric);

                if (
                    !ReferenceEquals(Set, null)
                    && !RoutineSet.FormulaSet.Contains(erubric)
                )
                {
                    RoutineSet.Rubrics.Remove(erubric);
                    Set = null;
                    Formula = null;
                }
            }
            return erubric;
        }
    }
}
