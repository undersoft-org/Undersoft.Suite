namespace Undersoft.SDK.Instant.Math.Set
{
    using Formulas;
    using SDK.Instant;
    using System.Reflection.Emit;
    using Undersoft.SDK.Instant.Math;
    using Undersoft.SDK.Instant.Series;

    [Serializable]
    public class SubMathSet : LeftFormula
    {
        public int startId = 0;

        public SubMathSet(MathSetFormula evalRubric, MathSet formuler)
        {
            if (evalRubric != null)
                Rubric = evalRubric;

            SetDimensions(formuler);
        }

        public int colCount
        {
            get { return Formuler.Rubrics.Count; }
        }

        public IInstantSeries Data
        {
            get { return Formuler.Data; }
        }

        public int FieldId
        {
            get => Rubric.InstantCreatorFieldId;
        }

        public MathSet Formuler { get; set; }

        public int rowCount
        {
            get { return Data.Count; }
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

        public override MathSetSize Size
        {
            get { return new MathSetSize(rowCount, colCount); }
        }

        public SubMathSet SubFormuler { get; set; }

        public override void Compile(ILGenerator g, InstantMathCompilerContext cc)
        {
            if (cc.IsFirstPass())
            {
                cc.Add(Data);
            }
            else
            {
                InstantMathCompilerContext.GenLocalLoad(g, cc.GetSubIndexOf(Data));

                g.Emit(OpCodes.Ldc_I4, FieldId);
                g.EmitCall(
                    OpCodes.Callvirt,
                    typeof(IInstant).GetMethod("get_Item", new Type[] { typeof(int) }),
                    null
                );
                g.Emit(OpCodes.Unbox_Any, RubricType);
                g.Emit(OpCodes.Conv_R8);
            }
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
                return;
            }

            int i1 = cc.GetIndexVariable(0);

            if (!post)
            {
                if (!partial)
                {
                    InstantMathCompilerContext.GenLocalLoad(g, cc.GetIndexOf(Data));

                    if (startId != 0)
                        g.Emit(OpCodes.Ldc_I4, startId);

                    g.Emit(OpCodes.Ldloc, i1);

                    if (startId != 0)
                        g.Emit(OpCodes.Add);

                    g.EmitCall(
                        OpCodes.Callvirt,
                        typeof(IInstantSeries).GetMethod("get_Item", new Type[] { typeof(int) }),
                        null
                    );
                    InstantMathCompilerContext.GenLocalStore(g, cc.GetSubIndexOf(Data));
                    InstantMathCompilerContext.GenLocalLoad(g, cc.GetSubIndexOf(Data));
                }
                else
                {
                    InstantMathCompilerContext.GenLocalLoad(g, cc.GetSubIndexOf(Data));
                }
                g.Emit(OpCodes.Ldc_I4, FieldId);
            }
            else
            {
                if (partial)
                {
                    g.Emit(OpCodes.Dup);
                    InstantMathCompilerContext.GenLocalStore(g, cc.GetBufforIndexOf(Data));
                }

                g.Emit(OpCodes.Box, typeof(double));
                g.EmitCall(
                    OpCodes.Callvirt,
                    typeof(IInstant).GetMethod(
                        "set_Item",
                        new Type[] { typeof(int), typeof(object) }
                    ),
                    null
                );

                if (partial)
                    InstantMathCompilerContext.GenLocalLoad(g, cc.GetBufforIndexOf(Data));
            }
        }

        public void SetDimensions(MathSet formuler = null)
        {
            if (!ReferenceEquals(formuler, null))
                Formuler = formuler;
            Rubric.SubSet = this;
        }
    }
}
