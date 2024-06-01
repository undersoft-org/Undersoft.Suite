namespace Undersoft.SDK.Instant.Math;

using Set;
using Formulas;
using System;
using System.Reflection;
using System.Reflection.Emit;

public class InstantMathCompiler
{
    internal static AssemblyBuilder ASSEMBLY;
    internal static int CLASS_ID;
    internal static bool COLLECT_MODE = false;
    internal static ModuleBuilder MODULE;
    internal static string TYPE_PREFIX = "MATHSET_";

    public static bool CollectMode
    {
        get { return COLLECT_MODE; }
        set
        {
            if (MODULE == null)
                COLLECT_MODE = value;
            else
            {
                throw new Exception("SaveMode cannot be more Changed!");
            }
        }
    }

    public static MathSetComputer Compile(Formula formula)
    {
        if (MODULE == null)
        {
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "EmittedAssembly";

            ASSEMBLY = AssemblyBuilder.DefineDynamicAssembly(
                assemblyName,
                CollectMode ? AssemblyBuilderAccess.RunAndCollect : AssemblyBuilderAccess.Run
            );
            MODULE = ASSEMBLY.DefineDynamicModule("EmittedModule");
            CLASS_ID = 0;
        }
        CLASS_ID++;

        TypeBuilder MathsetFormula = MODULE.DefineType(
            TYPE_PREFIX + CLASS_ID,
            TypeAttributes.Public,
            typeof(MathSetComputer)
        );
        Type[] constructorArgs = { };
        ConstructorBuilder constructor = MathsetFormula.DefineConstructor(
            MethodAttributes.Public,
            CallingConventions.Standard,
            null
        );

        ILGenerator constructorIL = constructor.GetILGenerator();
        constructorIL.Emit(OpCodes.Ldarg_0);
        ConstructorInfo superConstructor = typeof(object).GetConstructor(new Type[0]);
        constructorIL.Emit(OpCodes.Call, superConstructor);
        constructorIL.Emit(OpCodes.Ret);

        Type[] args = { };
        MethodBuilder fxMethod = MathsetFormula.DefineMethod(
            "Compute",
            MethodAttributes.Public | MethodAttributes.Virtual,
            typeof(void),
            args
        );
        ILGenerator methodIL = fxMethod.GetILGenerator();
        InstantMathCompilerContext context = new InstantMathCompilerContext();

        formula.Compile(methodIL, context);
        context.NextPass();
        context.GenerateLocalInit(methodIL);
        formula.Compile(methodIL, context);

        methodIL.Emit(OpCodes.Ret);

        Type mxt = MathsetFormula.CreateTypeInfo();
        MathSetComputer computation = (MathSetComputer)
            Activator.CreateInstance(mxt, new object[] { });
        computation.SetParams(context.ParamItems, context.Count);

        return computation;
    }
}
