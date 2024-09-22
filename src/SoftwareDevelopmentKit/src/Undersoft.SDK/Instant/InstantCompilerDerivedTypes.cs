namespace Undersoft.SDK.Instant
{
    using Extracting;
    using Rubrics;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.Serialization;
    using Undersoft.SDK;
    using Undersoft.SDK.Series;
    using Uniques;

    public class InstantCompilerDerivedTypes : InstantCompiler
    {
        public InstantCompilerDerivedTypes(InstantGenerator instantInstantGenerator, ISeries<MemberBuilder> memberBuilders)
            : base(instantInstantGenerator, memberBuilders) { }

        public override Type CompileInstantType(string typeName)
        {
            TypeBuilder tb = GetTypeBuilder(typeName);

            CreateCodeProperty(tb, typeof(Uscn), "Code");

            CreateChangesProperty(tb, typeof(MemberRubrics), "Changes");

            CreatePropertyChanged(tb);

            OverrideDerivedTypeProperty(tb);

            CreateFieldsAndProperties(tb);

            CreateValueArrayProperty(tb);

            CreateItemByIntProperty(tb);

            CreateItemByStringProperty(tb);

            CreateIdProperty(tb);

            CreateTypeIdProperty(tb);

            //CreateGetUniqueBytesMethod(tb);

            //CreateGetBytesMethod(tb);

            CreateEqualsMethod(tb);

            CreateCompareToMethod(tb);

            return tb.CreateTypeInfo();
        }

        public override void CreateCompareToMethod(TypeBuilder tb)
        {
            MethodInfo mi = typeof(IComparable<IUnique>).GetMethod("CompareTo");

            ParameterInfo[] args = mi.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                mi.Name,
                mi.Attributes & ~MethodAttributes.Abstract,
                mi.CallingConvention,
                mi.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(method, mi);

            ILGenerator il = method.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.Emit(OpCodes.Ldarg_1);
            il.EmitCall(
                OpCodes.Call,
                typeof(Uscn).GetMethod("CompareTo", new Type[] { typeof(IUnique) }),
                null
            );
            il.Emit(OpCodes.Ret);
        }

        public override void CreateEqualsMethod(TypeBuilder tb)
        {
            MethodInfo createArray = typeof(IEquatable<IUnique>).GetMethod("Equals");

            ParameterInfo[] args = createArray.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                createArray.Name,
                createArray.Attributes & ~MethodAttributes.Abstract,
                createArray.CallingConvention,
                createArray.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(method, createArray);

            ILGenerator il = method.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.Emit(OpCodes.Ldarg_1);
            il.EmitCall(
                OpCodes.Call,
                typeof(Uscn).GetMethod("Equals", new Type[] { typeof(IUnique) }),
                null
            );
            il.Emit(OpCodes.Ret);
        }

        public override void CreateFieldsAndProperties(TypeBuilder tb)
        {
            memberBuilders
                .AsValues()
                .ForEach(
                    (mr) =>
                    {
                        if (mr.Field != null)
                        {
                            if (!(mr.Field.IsBackingField))
                                ResolveMemberAttributes(null, mr.Field, mr.Member);
                            else if (mr.Property != null)
                                ResolveMemberAttributes(null, mr.Property, mr.Member);
                        }
                        else if (mr.Property != null)
                        {
                            ResolveMemberAttributes(null, mr.Property, mr.Member);
                        }
                    }
                );
        }

        public override void CreateGetBytesMethod(TypeBuilder tb)
        {
            MethodInfo createArray = typeof(IBinaryAccessor).GetMethod("GetBytes");

            ParameterInfo[] args = createArray.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                createArray.Name,
                createArray.Attributes & ~MethodAttributes.Abstract,
                createArray.CallingConvention,
                createArray.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(method, createArray);

            ILGenerator il = method.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Box, tb.UnderlyingSystemType);
            il.EmitCall(
                OpCodes.Call,
                typeof(ObjectExtractExtenstion).GetMethod(
                    "GetSequentialBytes",
                    new Type[] { typeof(object) }
                ),
                null
            );
            il.Emit(OpCodes.Ret);
        }

        public override void CreateGetEmptyProperty(TypeBuilder tb)
        {
            PropertyBuilder prop = tb.DefineProperty(
                "Empty",
                PropertyAttributes.HasDefault,
                typeof(IUnique),
                Type.EmptyTypes
            );

            PropertyInfo iprop = typeof(IUnique).GetProperty("Empty");

            MethodInfo accessor = iprop.GetGetMethod();

            ParameterInfo[] args = accessor.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder getter = tb.DefineMethod(
                accessor.Name,
                accessor.Attributes & ~MethodAttributes.Abstract,
                accessor.CallingConvention,
                accessor.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(getter, accessor);

            prop.SetGetMethod(getter);
            ILGenerator il = getter.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.EmitCall(OpCodes.Call, typeof(Uscn).GetMethod("get_Empty"), null);
            il.Emit(OpCodes.Ret);
        }

        public override void CreateGetIdBytesMethod(TypeBuilder tb)
        {
            MethodInfo createArray = typeof(IInstant).GetMethod("GetIdBytes");

            ParameterInfo[] args = createArray.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                createArray.Name,
                createArray.Attributes & ~MethodAttributes.Abstract,
                createArray.CallingConvention,
                createArray.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(method, createArray);

            ILGenerator il = method.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.EmitCall(OpCodes.Call, typeof(Uscn).GetMethod("GetIdBytes"), null);
            il.Emit(OpCodes.Ret);
        }

        public override void CreateItemByIntProperty(TypeBuilder tb)
        {
            foreach (PropertyInfo prop in typeof(IInstant).GetProperties())
            {
                MethodInfo accessor = prop.GetGetMethod();
                if (accessor != null)
                {
                    ParameterInfo[] args = accessor.GetParameters();
                    Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

                    if (args.Length == 1 && argTypes[0] == typeof(int))
                    {
                        MethodBuilder method = tb.DefineMethod(
                            accessor.Name,
                            accessor.Attributes & ~MethodAttributes.Abstract,
                            accessor.CallingConvention,
                            accessor.ReturnType,
                            argTypes
                        );
                        tb.DefineMethodOverride(method, accessor);
                        ILGenerator il = method.GetILGenerator();

                        Label[] branches = new Label[length];
                        for (int i = 0; i < length; i++)
                        {
                            branches[i] = il.DefineLabel();
                        }

                        il.Emit(OpCodes.Ldarg_1);

                        il.Emit(OpCodes.Switch, branches);

                        il.ThrowException(typeof(ArgumentOutOfRangeException));

                        for (int i = 0; i < length; i++)
                        {
                            var memberBuilder = memberBuilders[i];

                            if (memberBuilder.Type != null)
                            {
                                il.MarkLabel(branches[i]);

                                il.Emit(OpCodes.Ldarg_0);

                                if (
                                    memberBuilder.Field == null
                                    || memberBuilder.Field.IsBackingField
                                )
                                {
                                    il.EmitCall(OpCodes.Call, memberBuilder.Getter, null);
                                }
                                else
                                    il.Emit(OpCodes.Ldfld, memberBuilder.Field.RubricInfo);

                                if (memberBuilder.Type.IsValueType)
                                {
                                    il.Emit(OpCodes.Box, memberBuilder.Type);
                                }
                                il.Emit(OpCodes.Ret);
                            }
                        }
                    }
                }

                MethodInfo mutator = prop.GetSetMethod();
                if (mutator != null)
                {
                    ParameterInfo[] args = mutator.GetParameters();
                    Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

                    if (
                        args.Length == 2
                        && argTypes[0] == typeof(int)
                        && argTypes[1] == typeof(object)
                    )
                    {
                        MethodBuilder method = tb.DefineMethod(
                            mutator.Name,
                            mutator.Attributes & ~MethodAttributes.Abstract,
                            mutator.CallingConvention,
                            mutator.ReturnType,
                            argTypes
                        );
                        tb.DefineMethodOverride(method, mutator);
                        ILGenerator il = method.GetILGenerator();

                        Label[] branches = new Label[length];
                        for (int i = 0; i < length; i++)
                        {
                            branches[i] = il.DefineLabel();
                        }

                        il.Emit(OpCodes.Ldarg_1);

                        il.Emit(OpCodes.Switch, branches);

                        il.ThrowException(typeof(ArgumentOutOfRangeException));

                        for (int i = 0; i < length; i++)
                        {
                            var memberBuilder = memberBuilders[i];

                            il.MarkLabel(branches[i]);

                            il.Emit(OpCodes.Ldarg_0);
                            il.Emit(OpCodes.Ldarg_2);
                            il.Emit(
                                memberBuilder.Type.IsValueType
                                    ? OpCodes.Unbox_Any
                                    : OpCodes.Castclass,
                                memberBuilder.Type
                            );

                            if (
                                memberBuilder.Field == null
                                || memberBuilder.Field.IsBackingField
                            )
                            {
                                il.EmitCall(OpCodes.Call, memberBuilder.Setter, null);
                            }
                            else
                                il.Emit(OpCodes.Stfld, memberBuilder.Field.RubricInfo);

                            il.Emit(OpCodes.Ret);
                        }
                    }
                }
            }
        }

        public override void CreateItemByStringProperty(TypeBuilder tb)
        {
            foreach (PropertyInfo prop in typeof(IInstant).GetProperties())
            {
                MethodInfo accessor = prop.GetGetMethod();
                if (accessor != null)
                {
                    ParameterInfo[] args = accessor.GetParameters();
                    Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

                    if (args.Length == 1 && argTypes[0] == typeof(string))
                    {
                        MethodBuilder method = tb.DefineMethod(
                            accessor.Name,
                            accessor.Attributes & ~MethodAttributes.Abstract,
                            accessor.CallingConvention,
                            accessor.ReturnType,
                            argTypes
                        );
                        tb.DefineMethodOverride(method, accessor);
                        ILGenerator il = method.GetILGenerator();

                        il.DeclareLocal(typeof(string));

                        Label[] branches = new Label[length];

                        for (int i = 0; i < length; i++)
                        {
                            branches[i] = il.DefineLabel();
                        }

                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Stloc_0);

                        for (int i = 0; i < length; i++)
                        {
                            il.Emit(OpCodes.Ldloc_0);
                            il.Emit(OpCodes.Ldstr, memberBuilders[i].Name);
                            il.EmitCall(
                                OpCodes.Call,
                                typeof(string).GetMethod(
                                    "op_Equality",
                                    new Type[] { typeof(string), typeof(string) }
                                ),
                                null
                            );
                            il.Emit(OpCodes.Brtrue, branches[i]);
                        }

                        il.Emit(OpCodes.Ldnull);
                        il.Emit(OpCodes.Ret);

                        for (int i = 0; i < length; i++)
                        {
                            var memberBuilder = memberBuilders[i];

                            il.MarkLabel(branches[i]);
                            il.Emit(OpCodes.Ldarg_0);

                            if (
                                memberBuilder.Field == null
                                || memberBuilder.Field.IsBackingField
                            )
                            {
                                il.EmitCall(OpCodes.Call, memberBuilder.Getter, null);
                            }
                            else
                                il.Emit(OpCodes.Ldfld, memberBuilder.Field.RubricInfo);

                            if (memberBuilder.Type.IsValueType)
                            {
                                il.Emit(OpCodes.Box, memberBuilder.Type);
                            }
                            il.Emit(OpCodes.Ret);
                        }
                    }
                }

                MethodInfo mutator = prop.GetSetMethod();
                if (mutator != null)
                {
                    ParameterInfo[] args = mutator.GetParameters();
                    Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

                    if (
                        args.Length == 2
                        && argTypes[0] == typeof(string)
                        && argTypes[1] == typeof(object)
                    )
                    {
                        MethodBuilder method = tb.DefineMethod(
                            mutator.Name,
                            mutator.Attributes & ~MethodAttributes.Abstract,
                            mutator.CallingConvention,
                            mutator.ReturnType,
                            argTypes
                        );
                        tb.DefineMethodOverride(method, mutator);
                        ILGenerator il = method.GetILGenerator();

                        il.DeclareLocal(typeof(string));

                        Label[] branches = new Label[length];
                        for (int i = 0; i < length; i++)
                        {
                            branches[i] = il.DefineLabel();
                        }

                        il.Emit(OpCodes.Ldarg_1);

                        il.Emit(OpCodes.Stloc_0);

                        for (int i = 0; i < length; i++)
                        {
                            il.Emit(OpCodes.Ldloc_0);
                            il.Emit(OpCodes.Ldstr, memberBuilders[i].Name);
                            il.EmitCall(
                                OpCodes.Call,
                                typeof(string).GetMethod(
                                    "op_Equality",
                                    new[] { typeof(string), typeof(string) }
                                ),
                                null
                            );
                            il.Emit(OpCodes.Brtrue, branches[i]);
                        }

                        il.Emit(OpCodes.Ret);
                        for (int i = 0; i < length; i++)
                        {
                            var memberBuilder = memberBuilders[i];

                            il.MarkLabel(branches[i]);
                            il.Emit(OpCodes.Ldarg_0);
                            il.Emit(OpCodes.Ldarg_2);
                            il.Emit(
                                memberBuilder.Type.IsValueType
                                    ? OpCodes.Unbox_Any
                                    : OpCodes.Castclass,
                                memberBuilder.Type
                            );

                            if (
                                memberBuilder.Field == null
                                || memberBuilder.Field.IsBackingField
                            )
                            {
                                il.EmitCall(OpCodes.Call, memberBuilder.Setter, null);
                            }
                            else
                                il.Emit(OpCodes.Stfld, memberBuilder.Field.RubricInfo);

                            il.Emit(OpCodes.Ret);
                        }
                    }
                }
            }
        }

        public override void CreateCodeProperty(TypeBuilder tb, Type type, string name)
        {
            MemberBuilder fp = null;
            var field = memberBuilders
                .FirstOrDefault(
                    p =>
                        p.Field != null
                         && p.Field.FieldName.Equals(name, StringComparison.InvariantCultureIgnoreCase)
                );
            if (field != null)
            {
                scodeField = field.Field.RubricInfo;
            }
            else
            {
                FieldBuilder fb = createField(tb, null, type, name.ToLower());
                scodeField = fb;
                fp = new MemberBuilder(new MemberRubric(fb));
            }

            PropertyBuilder prop = tb.DefineProperty(
                name,
                PropertyAttributes.HasDefault,
                type,
                new Type[] { type }
            );

            PropertyInfo iprop = typeof(IInstant).GetProperty(name);

            MethodInfo accessor = iprop.GetGetMethod();

            ParameterInfo[] args = accessor.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder getter = tb.DefineMethod(
                accessor.Name,
                accessor.Attributes & ~MethodAttributes.Abstract,
                accessor.CallingConvention,
                accessor.ReturnType,
                argTypes
            );

            tb.DefineMethodOverride(getter, accessor);

            prop.SetGetMethod(getter);
            ILGenerator il = getter.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, scodeField);
            il.Emit(OpCodes.Ret);

            MethodInfo mutator = iprop.GetSetMethod();

            args = mutator.GetParameters();
            argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder setter = tb.DefineMethod(
                mutator.Name,
                mutator.Attributes & ~MethodAttributes.Abstract,
                mutator.CallingConvention,
                mutator.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(setter, mutator);

            prop.SetSetMethod(setter);
            il = setter.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, scodeField);
            il.Emit(OpCodes.Ret);

            prop.SetCustomAttribute(
                new CustomAttributeBuilder(
                    DataMemberCtor,
                    new object[0],
                    DataMemberProps,
                    new object[2] { 0, name.ToUpper() }
                )
            );
            if (fp != null)
            {
                fp.SetMember(new MemberRubric(prop));
                memberBuilders.Add(fp);
            }
            else if (field != null)
            {
                field.SetMember(new MemberRubric(prop));
            }
        }

        public override void CreateIdProperty(TypeBuilder tb)
        {
            PropertyBuilder prop = tb.DefineProperty(
                "Id",
                PropertyAttributes.HasDefault,
                typeof(long),
                new Type[] { typeof(long) }
            );

            PropertyInfo iprop = typeof(IIdentifiable).GetProperty("Id");

            MethodInfo accessor = iprop.GetGetMethod();

            ParameterInfo[] args = accessor.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder getter = tb.DefineMethod(
                accessor.Name,
                accessor.Attributes & ~MethodAttributes.Abstract,
                accessor.CallingConvention,
                accessor.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(getter, accessor);

            prop.SetGetMethod(getter);
            ILGenerator il = getter.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.EmitCall(OpCodes.Call, typeof(Uscn).GetProperty("Id").GetGetMethod(), null);
            il.Emit(OpCodes.Ret);

            MethodInfo mutator = iprop.GetSetMethod();

            args = mutator.GetParameters();
            argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder setter = tb.DefineMethod(
                mutator.Name,
                mutator.Attributes & ~MethodAttributes.Abstract,
                mutator.CallingConvention,
                mutator.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(setter, mutator);

            prop.SetSetMethod(setter);
            il = setter.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.Emit(OpCodes.Ldarg_1);
            il.EmitCall(OpCodes.Call, typeof(Uscn).GetProperty("Id").GetSetMethod(), null);
            il.Emit(OpCodes.Ret);
        }

        public override void CreateTypeIdProperty(TypeBuilder tb)
        {
            PropertyBuilder prop = tb.DefineProperty(
                "TypeId",
                PropertyAttributes.HasDefault,
                typeof(long),
                new Type[] { typeof(long) }
            );

            PropertyInfo iprop = typeof(IIdentifiable).GetProperty("TypeId");

            MethodInfo accessor = iprop.GetGetMethod();

            ParameterInfo[] args = accessor.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder getter = tb.DefineMethod(
                accessor.Name,
                accessor.Attributes & ~MethodAttributes.Abstract,
                accessor.CallingConvention,
                accessor.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(getter, accessor);

            prop.SetGetMethod(getter);
            ILGenerator il = getter.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.EmitCall(OpCodes.Call, typeof(Uscn).GetProperty("TypeId").GetGetMethod(), null);
            il.Emit(OpCodes.Ret);

            MethodInfo mutator = iprop.GetSetMethod();

            args = mutator.GetParameters();
            argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder setter = tb.DefineMethod(
                mutator.Name,
                mutator.Attributes & ~MethodAttributes.Abstract,
                mutator.CallingConvention,
                mutator.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(setter, mutator);

            prop.SetSetMethod(setter);
            il = setter.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.Emit(OpCodes.Ldarg_1);
            il.EmitCall(OpCodes.Call, typeof(Uscn).GetProperty("TypeId").GetSetMethod(), null);
            il.Emit(OpCodes.Ret);
        }

        public override void CreateValueArrayProperty(TypeBuilder tb)
        {
            PropertyInfo prop = typeof(IValueArray).GetProperty("ValueArray");

            MethodInfo accessor = prop.GetGetMethod();

            ParameterInfo[] args = accessor.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                accessor.Name,
                accessor.Attributes & ~MethodAttributes.Abstract,
                accessor.CallingConvention,
                accessor.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(method, accessor);

            ILGenerator il = method.GetILGenerator();
            il.DeclareLocal(typeof(object[]));

            il.Emit(OpCodes.Ldc_I4, length);
            il.Emit(OpCodes.Newarr, typeof(object));
            il.Emit(OpCodes.Stloc_0);

            for (int i = 0; i < length; i++)
            {
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg_0);

                if (memberBuilders[i].Field == null || memberBuilders[i].Field.IsBackingField)
                {
                    il.EmitCall(OpCodes.Call, memberBuilders[i].Getter, null);
                }
                else
                    il.Emit(OpCodes.Ldfld, memberBuilders[i].Field.RubricInfo);

                if (memberBuilders[i].Type.IsValueType)
                {
                    il.Emit(OpCodes.Box, memberBuilders[i].Type);
                }

                il.Emit(OpCodes.Stelem, typeof(object));
            }
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            MethodInfo mutator = prop.GetSetMethod();

            args = mutator.GetParameters();
            argTypes = Array.ConvertAll(args, a => a.ParameterType);

            method = tb.DefineMethod(
                mutator.Name,
                mutator.Attributes & ~MethodAttributes.Abstract,
                mutator.CallingConvention,
                mutator.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(method, mutator);
            il = method.GetILGenerator();
            il.DeclareLocal(typeof(object[]));

            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stloc_0);

            for (int i = 0; i < length; i++)
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldelem, typeof(object));
                il.Emit(
                    memberBuilders[i].Type.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass,
                    memberBuilders[i].Type
                );

                if (memberBuilders[i].Field == null || memberBuilders[i].Field.IsBackingField)
                {
                    il.EmitCall(OpCodes.Call, memberBuilders[i].Setter, null);
                }
                else
                    il.Emit(OpCodes.Stfld, memberBuilders[i].Field.RubricInfo);
            }
            il.Emit(OpCodes.Ret);
        }

        public override TypeBuilder GetTypeBuilder(string typeName)
        {
            string typeSignature =
                (typeName != null && typeName != "") ? typeName : Unique.NewId.ToString();
            AssemblyName an = new AssemblyName(typeSignature);

            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                an,
                AssemblyBuilderAccess.RunAndCollect
            );
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(
                typeSignature + "Module"
            );
            TypeBuilder tb = null;

            tb = moduleBuilder.DefineType(
                typeSignature,
                TypeAttributes.AutoClass
                    | TypeAttributes.Public
                    | TypeAttributes.BeforeFieldInit
                    | TypeAttributes.AutoLayout
            );

            tb.SetCustomAttribute(
                new CustomAttributeBuilder(
                    typeof(DataContractAttribute).GetConstructor(Type.EmptyTypes),
                    new object[0]
                )
            );

            tb.AddInterfaceImplementation(typeof(IValueArray));
            tb.AddInterfaceImplementation(typeof(IInstant));

            tb.SetParent(figure.BaseType);

            return tb;
        }

        private FieldBuilder createField(
            TypeBuilder tb,
            MemberRubric mr,
            Type type,
            string fieldName
        )
        {
            if (type == typeof(string) || type.IsArray)
            {
                FieldBuilder fb = tb.DefineField(
                    fieldName,
                    type,
                    FieldAttributes.Private
                        | FieldAttributes.HasDefault
                        | FieldAttributes.HasFieldMarshal
                );

                if (type == typeof(string))
                    ResolveMarshalAsAttributeForString(fb, mr, type);
                else
                    ResolveMarshalAsAttributeForArray(fb, mr, type);

                return fb;
            }
            else
            {
                return tb.DefineField(fieldName, type, FieldAttributes.Private);
            }
        }
    }
}
