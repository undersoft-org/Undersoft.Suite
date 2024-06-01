namespace Undersoft.SDK.Instant.Math.Set
{
    using System.Linq;
    using SDK.Series;
    using SDK.Series.Base;
    using Set;
    using Rubrics;
    using Undersoft.SDK.Instant.Series;

    public class MathSetRoutine : ListingBase<MathSetFormula>
    {
        public MathSetRoutine(IInstantSeries data)
        {
            Rubrics = data.Rubrics;
            FormulaSet = new MathSetRoutine(Rubrics);
            RoutineSet = new MathSetRoutine(Rubrics);
            Data = data;
        }

        public MathSetRoutine(IRubrics rubrics)
        {
            Rubrics = rubrics;
            Data = rubrics.Series;
        }

        public MathSetRoutine(MathSetRoutine rubrics)
        {
            Rubrics = rubrics.Rubrics;
            Data = rubrics.Data;
        }

        public IInstantSeries Data { get; set; }

        public MathSetRoutine FormulaSet { get; set; }

        public MathSetRoutine RoutineSet { get; set; }

        public int RowsCount
        {
            get { return Data.Count; }
        }

        public IRubrics Rubrics { get; set; }

        public int RubricsCount
        {
            get { return Rubrics.Count; }
        }

        public bool Combine(int offset = 0, int batch = 0)
        {
            if (!ReferenceEquals(Data, null))
            {
                MathSetComputer[] evs = CombineEvaluators();
                evs.ForEach(e => e.SetParams(Data, 0, offset, batch));
                return true;
            }
            else
                CombineEvaluators();
            return false;
        }

        public bool Combine(IInstantSeries table, int offset = 0, int batch = 0)
        {
            if (!ReferenceEquals(Data, table))
            {
                Data = table;
                MathSetComputer[] evs = CombineEvaluators();
                evs.ForEach(e => e.SetParams(Data, 0, offset, batch));
                return true;
            }
            else
                CombineEvaluators();
            return false;
        }

        public MathSetComputer[] Compile(int offset = 0, int batch = 0)
        {
            if (!ReferenceEquals(Data, null))
            {
                MathSetComputer[] evs = CompileEvaluators();
                evs.ForEach(e => e.SetParams(Data, 0, offset, batch));
                return evs;
            }
            else
                return CompileEvaluators();
        }

        public MathSetComputer[] Compile(IInstantSeries table, int offset = 0, int batch = 0)
        {
            if (!ReferenceEquals(Data, table))
            {
                Data = table;
                MathSetComputer[] evs = CompileEvaluators();
                evs.ForEach(e => e.SetParams(Data, 0, offset, batch));
                return evs;
            }
            else
                return CompileEvaluators();
        }

        public MathSetComputer[] CombineEvaluators()
        {
            return AsValues().Select(m => m.CombineEvaluator()).ToArray();
        }

        public MathSetComputer[] CompileEvaluators()
        {
            return AsValues().Select(m => m.GetCompiledMathSet()).ToArray();
        }

        public override ISeriesItem<MathSetFormula>[] EmptyVector(int size)
        {
            return new MathSetItem[size];
        }

        public override ISeriesItem<MathSetFormula> EmptyItem()
        {
            return new MathSetItem();
        }

        public override ISeriesItem<MathSetFormula>[] EmptyTable(int size)
        {
            return new MathSetItem[size];
        }

        public override ISeriesItem<MathSetFormula> NewItem(ISeriesItem<MathSetFormula> value)
        {
            return new MathSetItem(value);
        }

        public override ISeriesItem<MathSetFormula> NewItem(MathSetFormula value)
        {
            return new MathSetItem(value);
        }

        public override ISeriesItem<MathSetFormula> NewItem(object key, MathSetFormula value)
        {
            return new MathSetItem(key, value);
        }

        public override ISeriesItem<MathSetFormula> NewItem(long key, MathSetFormula value)
        {
            return new MathSetItem(key, value);
        }
    }
}
