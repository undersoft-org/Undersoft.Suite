namespace Undersoft.SDK.Instant
{
    using Attributes;
    using Rubrics;
    using System.ComponentModel;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;
    using Undersoft.SDK;
    using Undersoft.SDK.Series;
    using Uniques;

    public enum InstantType
    {
        Derived,
        ValueType,
        Reference
    }

    public abstract class InstantCompiler : InstantCompilerBase
    {
        protected InstantGenerator figure;
        protected int length;
        protected InstantType mode;
        protected FieldInfo scodeField;
        protected FieldInfo changesField;
        protected MethodBuilder raisePropertyChanged;

        public ISeries<MemberBuilder> memberBuilders;

        public InstantCompiler(
            InstantGenerator instantInstantGenerator,
            ISeries<MemberBuilder> rubricBuilders
        )
        {
            this.memberBuilders = rubricBuilders;
            figure = instantInstantGenerator;
            length = rubricBuilders.Count;
        }

        protected bool IsDerived => figure.IsDerived;

        public abstract Type CompileInstantType(string typeName);

        public abstract TypeBuilder GetTypeBuilder(string typeName);

        public abstract void CreateFieldsAndProperties(TypeBuilder tb);

        public abstract void CreateGetBytesMethod(TypeBuilder tb);

        public abstract void CreateItemByIntProperty(TypeBuilder tb);

        public abstract void CreateItemByStringProperty(TypeBuilder tb);

        public abstract void CreateCodeProperty(TypeBuilder tb, Type type, string name);

        public abstract void CreateValueArrayProperty(TypeBuilder tb);

        public virtual void CreateChangesProperty(TypeBuilder tb, Type type, string name)
        {
            changesField = tb.DefineField(name.ToLower(), type, FieldAttributes.Private);

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
            il.Emit(OpCodes.Ldfld, changesField);
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
            il.Emit(OpCodes.Stfld, changesField);
            il.Emit(OpCodes.Ret);

            prop.SetCustomAttribute(
                new CustomAttributeBuilder(
                    DataMemberCtor,
                    new object[0],
                    DataMemberProps,
                    new object[2] { length + 1, name.ToUpper() }
                )
            );
        }

        public virtual void CreateCompareToMethod(TypeBuilder tb)
        {
            MethodInfo mi = typeof(IComparable<IUnique>).GetMethod("CompareTo");

            ParameterInfo[] args = mi.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                mi.Name,
                mi.Attributes & (~MethodAttributes.Abstract),
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

        public virtual void CreateEqualsMethod(TypeBuilder tb)
        {
            MethodInfo createArray = typeof(IEquatable<IUnique>).GetMethod("Equals");

            ParameterInfo[] args = createArray.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                createArray.Name,
                createArray.Attributes & (~MethodAttributes.Abstract),
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

        public virtual void CreateRubricAsAttribute(FieldBuilder field, InstantAsAttribute attrib)
        {
            field.SetCustomAttribute(
                new CustomAttributeBuilder(
                    MarshalAsCtor,
                    new object[] { attrib.Value },
                    new FieldInfo[] { typeof(MarshalAsAttribute).GetField("SizeConst") },
                    new object[] { attrib.SizeConst }
                )
            );
        }

        public virtual void CreateGetEmptyProperty(TypeBuilder tb)
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
                accessor.Attributes & (~MethodAttributes.Abstract),
                accessor.CallingConvention,
                accessor.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(getter, accessor);

            ILGenerator il = getter.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.EmitCall(OpCodes.Call, typeof(Uscn).GetMethod("get_Empty"), null);
            il.Emit(OpCodes.Ret);
        }

        public virtual void CreateGetGenericByIntMethod(TypeBuilder tb)
        {
            string[] typeParameterNames = { "V" };
            GenericTypeParameterBuilder[] typeParameters = tb.DefineGenericParameters(
                typeParameterNames
            );

            GenericTypeParameterBuilder V = typeParameters[0];

            MethodInfo mi = typeof(IInstant)
                .GetMethod("Get", new Type[] { typeof(int) })
                .MakeGenericMethod(typeParameters);

            ParameterInfo[] args = mi.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                mi.Name,
                mi.Attributes & (~MethodAttributes.Abstract),
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

        public virtual void CreateGetIdBytesMethod(TypeBuilder tb)
        {
            MethodInfo createArray = typeof(IUnique).GetMethod("GetIdBytes");

            ParameterInfo[] args = createArray.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                createArray.Name,
                createArray.Attributes & (~MethodAttributes.Abstract),
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

        public virtual void CreateGetIdMethod(TypeBuilder tb)
        {
            MethodInfo createArray = typeof(IUnique).GetMethod("GetId");

            ParameterInfo[] args = createArray.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                createArray.Name,
                createArray.Attributes & (~MethodAttributes.Abstract),
                createArray.CallingConvention,
                createArray.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(method, createArray);

            ILGenerator il = method.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.EmitCall(OpCodes.Call, typeof(Uscn).GetMethod("GetId"), null);
            il.Emit(OpCodes.Ret);
        }

        public virtual void CreateGetTypeIdMethod(TypeBuilder tb)
        {
            MethodInfo createArray = typeof(IUnique).GetMethod("GetTypeId");

            ParameterInfo[] args = createArray.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                createArray.Name,
                createArray.Attributes & (~MethodAttributes.Abstract),
                createArray.CallingConvention,
                createArray.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(method, createArray);

            ILGenerator il = method.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.EmitCall(OpCodes.Call, typeof(Uscn).GetMethod("GetTypeId"), null);
            il.Emit(OpCodes.Ret);
        }

        public virtual void CreateSetIdMethod(TypeBuilder tb)
        {
            MethodInfo createArray = typeof(IUnique).GetMethod("SetId");

            ParameterInfo[] args = createArray.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                createArray.Name,
                createArray.Attributes & (~MethodAttributes.Abstract),
                createArray.CallingConvention,
                createArray.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(method, createArray);

            ILGenerator il = method.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.EmitCall(OpCodes.Call, typeof(Uscn).GetMethod("SetId"), null);
            il.Emit(OpCodes.Ret);
        }

        public virtual void CreateSetTypeIdMethod(TypeBuilder tb)
        {
            MethodInfo createArray = typeof(IUnique).GetMethod("SetTypeId");

            ParameterInfo[] args = createArray.GetParameters();
            Type[] argTypes = Array.ConvertAll(args, a => a.ParameterType);

            MethodBuilder method = tb.DefineMethod(
                createArray.Name,
                createArray.Attributes & (~MethodAttributes.Abstract),
                createArray.CallingConvention,
                createArray.ReturnType,
                argTypes
            );
            tb.DefineMethodOverride(method, createArray);

            ILGenerator il = method.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldflda, scodeField);
            il.EmitCall(OpCodes.Call, typeof(Uscn).GetMethod("SetTypeId"), null);
            il.Emit(OpCodes.Ret);
        }

        public virtual void CreateIdProperty(TypeBuilder tb)
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
                accessor.Attributes & (~MethodAttributes.Abstract),
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
                mutator.Attributes & (~MethodAttributes.Abstract),
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

        public virtual void CreateTypeIdProperty(TypeBuilder tb)
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
                accessor.Attributes & (~MethodAttributes.Abstract),
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
                mutator.Attributes & (~MethodAttributes.Abstract),
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

        public virtual void CreatePropertyChanged(TypeBuilder tb)
        {
            tb.AddInterfaceImplementation(typeof(INotifyPropertyChanged));

            FieldBuilder eventField = CreatePropertyChangedEvent(tb);

            raisePropertyChanged = CreateRaisePropertyChanged(tb, eventField);
        }

        public virtual void OverrideDerivedTypeProperty(TypeBuilder tb)
        {
            var props = memberBuilders
             .Where(
                 p =>
                     p.Property != null && p.Property.GetSetMethod() != null
                    && p.Property.SetMethod.IsVirtual && !p.Property.SetMethod.IsFinal
                    && (p.Property.SetMethod.IsPublic || p.Property.SetMethod.IsFamily)
             )
             .Select(p => p.Property);

            props.ToList().ForEach((item) => WrapMethod(item, raisePropertyChanged, tb));
        }

        private FieldBuilder CreatePropertyChangedEvent(TypeBuilder tb)
        {
            // public event PropertyChangedEventHandler PropertyChanged;

            FieldBuilder eventField = tb.DefineField(
                "PropertyChanged",
                typeof(PropertyChangedEventHandler),
                FieldAttributes.Private
            );
            EventBuilder eventBuilder = tb.DefineEvent(
                "PropertyChanged",
                EventAttributes.None,
                typeof(PropertyChangedEventHandler)
            );

            eventBuilder.SetAddOnMethod(CreateAddRemoveMethod(tb, eventField, true));
            eventBuilder.SetRemoveOnMethod(CreateAddRemoveMethod(tb, eventField, false));

            return eventField;
        }

        private MethodBuilder CreateAddRemoveMethod(
            TypeBuilder typeBuilder,
            FieldBuilder eventField,
            bool isAdd
        )
        {
            string prefix = "remove_";
            string delegateAction = "Remove";
            if (isAdd)
            {
                prefix = "add_";
                delegateAction = "Combine";
            }
            MethodBuilder addremoveMethod = typeBuilder.DefineMethod(
                prefix + "PropertyChanged",
                MethodAttributes.Public
                    | MethodAttributes.SpecialName
                    | MethodAttributes.NewSlot
                    | MethodAttributes.HideBySig
                    | MethodAttributes.Virtual
                    | MethodAttributes.Final,
                null,
                new[] { typeof(PropertyChangedEventHandler) }
            );
            MethodImplAttributes eventMethodFlags =
                MethodImplAttributes.Managed | MethodImplAttributes.Synchronized;
            addremoveMethod.SetImplementationFlags(eventMethodFlags);

            ILGenerator ilGen = addremoveMethod.GetILGenerator();

            // PropertyChanged += value; // PropertyChanged -= value;
            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Ldfld, eventField);
            ilGen.Emit(OpCodes.Ldarg_1);
            ilGen.EmitCall(
                OpCodes.Call,
                typeof(Delegate).GetMethod(
                    delegateAction,
                    new[] { typeof(Delegate), typeof(Delegate) }
                ),
                null
            );
            ilGen.Emit(OpCodes.Castclass, typeof(PropertyChangedEventHandler));
            ilGen.Emit(OpCodes.Stfld, eventField);
            ilGen.Emit(OpCodes.Ret);

            MethodInfo intAddRemoveMethod = typeof(INotifyPropertyChanged).GetMethod(
                prefix + "PropertyChanged"
            );
            typeBuilder.DefineMethodOverride(addremoveMethod, intAddRemoveMethod);

            return addremoveMethod;
        }

        private MethodBuilder CreateRaisePropertyChanged(TypeBuilder tb, FieldBuilder eventField)
        {
            MethodBuilder raisePropertyChangedBuilder = tb.DefineMethod(
                "RaisePropertyChanged",
                MethodAttributes.Family | MethodAttributes.Virtual,
                null,
                new Type[] { typeof(string) }
            );

            ILGenerator raisePropertyChangedIl = raisePropertyChangedBuilder.GetILGenerator();
            Label labelExit = raisePropertyChangedIl.DefineLabel();

            // if (PropertyChanged == null)
            // {
            //      return;
            // }
            raisePropertyChangedIl.Emit(OpCodes.Ldarg_0);
            raisePropertyChangedIl.Emit(OpCodes.Ldfld, eventField);
            raisePropertyChangedIl.Emit(OpCodes.Ldnull);
            raisePropertyChangedIl.Emit(OpCodes.Ceq);
            raisePropertyChangedIl.Emit(OpCodes.Brtrue, labelExit);

            // this.PropertyChanged(this,
            // new PropertyChangedEventArgs(propertyName));
            raisePropertyChangedIl.Emit(OpCodes.Ldarg_0);
            raisePropertyChangedIl.Emit(OpCodes.Ldfld, eventField);
            raisePropertyChangedIl.Emit(OpCodes.Ldarg_0);
            raisePropertyChangedIl.Emit(OpCodes.Ldarg_1);
            raisePropertyChangedIl.Emit(
                OpCodes.Newobj,
                typeof(PropertyChangedEventArgs).GetConstructor(new[] { typeof(string) })
            );
            raisePropertyChangedIl.EmitCall(
                OpCodes.Callvirt,
                typeof(PropertyChangedEventHandler).GetMethod("Invoke"),
                null
            );

            // return;
            raisePropertyChangedIl.MarkLabel(labelExit);
            raisePropertyChangedIl.Emit(OpCodes.Ret);

            return raisePropertyChangedBuilder;
        }

        private void WrapMethod(
            PropertyInfo item,
            MethodBuilder raisePropertyChanged,
            TypeBuilder tb
        )
        {
            MethodInfo setMethod = item.GetSetMethod();

            //get an array of the parameter types.
            var types = from t in setMethod.GetParameters() select t.ParameterType;

            MethodBuilder setMethodBuilder = tb.DefineMethod(
                setMethod.Name,
                setMethod.Attributes,
                setMethod.ReturnType,
                types.ToArray()
            );
            tb.DefineMethodOverride(setMethodBuilder, setMethod);
            ILGenerator setMethodWrapperIl = setMethodBuilder.GetILGenerator();

            // base.[PropertyName] = value;
            setMethodWrapperIl.Emit(OpCodes.Ldarg_0);
            setMethodWrapperIl.Emit(OpCodes.Ldarg_1);
            setMethodWrapperIl.EmitCall(OpCodes.Call, setMethod, null);

            // RaisePropertyChanged("[PropertyName]");
            setMethodWrapperIl.Emit(OpCodes.Ldarg_0);
            setMethodWrapperIl.Emit(OpCodes.Ldstr, item.Name);
            setMethodWrapperIl.EmitCall(OpCodes.Call, raisePropertyChanged, null);

            // return;
            setMethodWrapperIl.Emit(OpCodes.Ret);
        }
    }
}
