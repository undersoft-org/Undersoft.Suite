using Undersoft.SDK.Instant.Series;

namespace Undersoft.SDK.Instant.Math;

using Set;
using System;
using System.Reflection.Emit;

public delegate void Evaluator();

[Serializable]
public class InstantMathCompilerContext
{
    [NonSerialized]
    internal int indexVariableCount;

    [NonSerialized]
    internal int[] indexVariables;

    [NonSerialized]
    internal int paramCount;

    [NonSerialized]
    internal IInstantSeries[] paramTables = new IInstantSeries[10];

    [NonSerialized]
    internal int pass = 0;

    public InstantMathCompilerContext()
    {
        indexVariableCount = 0;
    }

    public int Count
    {
        get { return paramCount; }
    }

    public IInstantSeries[] ParamItems
    {
        get { return paramTables; }
    }

    public static void GenLocalLoad(ILGenerator g, int a)
    {
        switch (a)
        {
            case 0:
                g.Emit(OpCodes.Ldloc_0);
                break;
            case 1:
                g.Emit(OpCodes.Ldloc_1);
                break;
            case 2:
                g.Emit(OpCodes.Ldloc_2);
                break;
            case 3:
                g.Emit(OpCodes.Ldloc_3);
                break;
            default:
                g.Emit(OpCodes.Ldloc, a);
                break;
        }
    }

    public static void GenLocalStore(ILGenerator g, int a)
    {
        switch (a)
        {
            case 0:
                g.Emit(OpCodes.Stloc_0);
                break;
            case 1:
                g.Emit(OpCodes.Stloc_1);
                break;
            case 2:
                g.Emit(OpCodes.Stloc_2);
                break;
            case 3:
                g.Emit(OpCodes.Stloc_3);
                break;
            default:
                g.Emit(OpCodes.Stloc, a);
                break;
        }
    }

    public int Add(IInstantSeries v)
    {
        int index = GetIndexOf(v);
        if (index < 0)
        {
            paramTables[paramCount] = v;
            return indexVariableCount + paramCount++;
        }
        return index;
    }

    public int AllocIndexVariable()
    {
        return indexVariableCount++;
    }

    public void GenerateLocalInit(ILGenerator g)
    {
        for (int i = 0; i < indexVariableCount; i++)
            g.DeclareLocal(typeof(int));

        string paramFieldName = "DataParameters";

        for (int i = 0; i < paramCount; i++)
            g.DeclareLocal(typeof(IInstantSeries));

        for (int i = 0; i < paramCount; i++)
            g.DeclareLocal(typeof(IInstant));

        g.DeclareLocal(typeof(double));

        for (int i = 0; i < paramCount; i++)
        {
            g.Emit(OpCodes.Ldarg_0);
            g.Emit(OpCodes.Ldfld, typeof(MathSetComputer).GetField(paramFieldName));
            g.Emit(OpCodes.Ldc_I4, i);
            g.Emit(OpCodes.Ldelem_Ref);
            g.Emit(OpCodes.Stloc, indexVariableCount + i);
        }
    }

    public int GetBufforIndexOf(IInstantSeries v)
    {
        for (int i = 0; i < paramCount; i++)
            if (paramTables[i] == v)
                return indexVariableCount + i + paramCount + 1;
        return -1;
    }

    public int GetIndexOf(IInstantSeries v)
    {
        for (int i = 0; i < paramCount; i++)
            if (paramTables[i] == v)
                return indexVariableCount + i;
        return -1;
    }

    public int GetIndexVariable(int number)
    {
        return indexVariables[number];
    }

    public int GetSubIndexOf(IInstantSeries v)
    {
        for (int i = 0; i < paramCount; i++)
            if (paramTables[i] == v)
                return indexVariableCount + i + paramCount;
        return -1;
    }

    public bool IsFirstPass()
    {
        return pass == 0;
    }

    public void NextPass()
    {
        pass++;

        indexVariables = new int[indexVariableCount];
        for (int i = 0; i < indexVariableCount; i++)
            indexVariables[i] = i;
    }

    public void SetIndexVariable(int number, int value)
    {
        indexVariables[number] = value;
    }
}
