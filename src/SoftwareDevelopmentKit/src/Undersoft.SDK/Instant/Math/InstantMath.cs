namespace Undersoft.SDK.Instant.Math;

using Rubrics;
using SDK.Series;
using Set;
using System.Linq;
using System.Linq.Expressions;
using Undersoft.SDK;
using Undersoft.SDK.Extracting;
using Undersoft.SDK.Instant.Series;
using Uniques;

public class InstantMath<T> : InstantMath
{
    public InstantMath(IInstantSeries data) : base(data)
    {
    }

    public InstantMath(MathSetRoutine routines) : base(routines)
    {
    }

    public MathSet<T> this[Expression<Func<T, object>> exp]
    {
        get
        {
            return GetMathSet(exp);
        }
    }

    public MathSet<T> GetMathSet(Expression<Func<T, object>> exp)
    {
        var name = exp.GetMemberName();
        MemberRubric rubric = null;
        if (Routine.Rubrics.TryGet(name, out rubric))
        {
            MathSetFormula mathrubric = null;
            if (Routine.RoutineSet.TryGet(name, out mathrubric))
                return mathrubric.GetMathset<T>();
            return Routine.Put(rubric.Name, new MathSetFormula(Routine, rubric)).Value.GetMathset<T>();
        }
        return null;
    }
}

public class InstantMath : Identifiable, IInstantMath
{
    private MathSetRoutine _routine;

    public InstantMath(IInstantSeries data)
    {
        _routine = new MathSetRoutine(data);
        if (data.Computations == null)
            data.Computations = new Catalog<IInstantMath>();
        data.Computations.Put(this);
    }

    public InstantMath(MathSetRoutine routines)
    {
        _routine = routines;
        if (routines.Data.Computations == null)
            routines.Data.Computations = new Catalog<IInstantMath>();
        routines.Data.Computations.Put(this);
    }

    public MathSet this[int id]
    {
        get { return GetMathSet(id); }
    }
    public MathSet this[string name]
    {
        get { return GetMathSet(name); }
    }
    public MathSet this[MemberRubric rubric]
    {
        get { return GetMathSet(rubric); }
    }

    public MathSet GetMathSet(int id)
    {
        MemberRubric rubric = _routine.Rubrics[id];
        if (id < _routine.RubricsCount)
        {
            MathSetFormula mathrubric = null;
            if (_routine.RoutineSet.TryGet(rubric.Name, out mathrubric))
                return mathrubric.GetMathset();
            return _routine.Put(rubric.Name, new MathSetFormula(_routine, rubric)).Value.GetMathset();
        }
        return null;
    }

    public MathSet GetMathSet(string name)
    {
        MemberRubric rubric = null;
        if (_routine.Rubrics.TryGet(name, out rubric))
        {
            MathSetFormula mathrubric = null;
            if (_routine.RoutineSet.TryGet(name, out mathrubric))
                return mathrubric.GetMathset();
            return _routine.Put(rubric.Name, new MathSetFormula(_routine, rubric)).Value.GetMathset();
        }
        return null;
    }

    public MathSet GetMathSet(MemberRubric rubric)
    {
        return GetMathSet(rubric.Name);
    }

    public bool ContainsFirst(MemberRubric rubric)
    {
        return _routine.First.Value.RubricName == rubric.Name;
    }

    public bool ContainsFirst(string rubricName)
    {
        return _routine.First.Value.RubricName == rubricName;
    }

    public IInstantSeries Compute()
    {
        _routine.Combine();
        _routine
            .Where(p => !p.PartialMathset)
            .OrderBy(p => p.ComputeOrdinal)
            .Select(p => p.Compute())
            .ToArray();
        return _routine.Data;
    }

    public IInstantSeries Compute(int chunks)
    {
        var rowcount = _routine.Data.Count;
        var chunksize = rowcount / chunks;
        var lastchunksize = rowcount - chunksize * chunks + chunksize;
        InstantMath _temp = this;
        MathSetComputer[][] mathChunks = new MathSetComputer[chunks][];

        mathChunks.AsParallel().ForEach(
            (ism, x) =>
            {
                mathChunks[x] = _temp.Compile(x * chunksize, x == chunks - 1 ? lastchunksize : chunksize);
                _temp.Clone();
            }
        );

        mathChunks.AsParallel().ForEach((mch) => mch.ForEach((cms) => new Evaluator(cms.Compute)()));
        return _routine.Data;
    }

    public MathSetComputer[] Compile(int offset, int chunk)
    {
        return _routine.Compile(offset, chunk);
    }

    public InstantMath Clone()
    {
        return new InstantMath(_routine);
    }

    public MathSetRoutine Routine => _routine;

    public int CompareTo(IUnique other)
    {
        return Id.CompareTo(other.Id);
    }

    public bool Equals(IUnique other)
    {
        return Id.Equals(other.Id);
    }

    public byte[] GetBytes()
    {
        return Id.GetBytes();
    }

    public byte[] GetIdBytes()
    {
        return Id.GetBytes();
    }
}
