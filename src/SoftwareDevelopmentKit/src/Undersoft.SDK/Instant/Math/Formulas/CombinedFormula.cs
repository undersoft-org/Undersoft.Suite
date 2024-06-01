namespace Undersoft.SDK.Instant.Math.Formulas
{
    using System;
    using System.Reflection.Emit;
    using Set;
    using Undersoft.SDK.Instant.Math;

    [Serializable]
    public class CombinedFormula : Formula
    {
        public Formula expr;
        public LeftFormula lexpr;
        public bool partial = false;
        internal int iI,
            lL;

        public CombinedFormula(LeftFormula m, Formula e, bool partial = false)
        {
            lexpr = m;
            expr = e;
            this.partial = partial;
        }

        internal MathSetSize size
        {
            get { return expr.Size; }
        }

        public override void Compile(ILGenerator g, InstantMathCompilerContext cc)
        {
            bool biloop = size.rows > 1 && size.cols > 1;

            if (cc.IsFirstPass())
            {
                if (!partial)
                {
                    iI = cc.AllocIndexVariable();
                    lL = cc.AllocIndexVariable();
                }
                expr.Compile(g, cc);
                lexpr.CompileAssign(g, cc, true, partial);
            }
            else
            {
                if (!partial)
                {
                    int i,
                        l,
                        svi;
                    Label topLabel;
                    Label topLabelE;

                    topLabel = g.DefineLabel();
                    topLabelE = g.DefineLabel();

                    i = cc.GetIndexVariable(iI);
                    l = cc.GetIndexVariable(lL);

                    svi = cc.GetIndexVariable(0);

                    cc.SetIndexVariable(0, i);
                    cc.SetIndexVariable(1, size.rows);

                    g.Emit(OpCodes.Ldarg_0);
                    g.EmitCall(
                       OpCodes.Callvirt,
                       typeof(MathSetComputer).GetMethod(
                           "GetRowOffset"
                       ),
                       null
                   );
                    g.Emit(OpCodes.Stloc, i);
                    g.Emit(OpCodes.Ldarg_0);
                    g.Emit(OpCodes.Ldc_I4_0);
                    g.EmitCall(
                        OpCodes.Callvirt,
                        typeof(MathSetComputer).GetMethod(
                            "GetRowCount",
                            new Type[] { typeof(int) }
                        ),
                        null
                    );
                    g.Emit(OpCodes.Stloc, l);

                    if (size.rows > 1 || size.cols > 1)
                    {

                        g.MarkLabel(topLabel);

                        lexpr.CompileAssign(g, cc, false, false);
                        expr.Compile(g, cc);
                        lexpr.CompileAssign(g, cc, true, false);

                        g.Emit(OpCodes.Ldloc, i);
                        g.Emit(OpCodes.Ldc_I4_1);
                        g.Emit(OpCodes.Add);
                        g.Emit(OpCodes.Dup);
                        g.Emit(OpCodes.Stloc, i);

                        g.Emit(OpCodes.Ldloc, l);
                        g.Emit(OpCodes.Blt, topLabel);
                    }
                    else
                    {
                        lexpr.CompileAssign(g, cc, false, false);
                        expr.Compile(g, cc);
                        lexpr.CompileAssign(g, cc, true, false);
                    }
                    cc.SetIndexVariable(0, svi);
                }
                else
                {
                    lexpr.CompileAssign(g, cc, false, true);
                    expr.Compile(g, cc);
                    lexpr.CompileAssign(g, cc, true, true);
                }
            }
        }
    }
}
