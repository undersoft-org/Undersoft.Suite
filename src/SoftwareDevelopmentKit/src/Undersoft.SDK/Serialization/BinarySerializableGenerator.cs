using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Serialization
{
    public class BinarySerializableGenerator
    {
        private static Dictionary<string, TypeBuilder> _cache = new Dictionary<string, TypeBuilder>();

        private static string GetBinaryReaderMethod(Type t)
        {
            if (t == typeof(Int32))
                return "ReadInt32";

            if (t == typeof(UInt32))
                return "ReadUInt32";

            if (t == typeof(UInt64))
                return "ReadUInt64";

            if (t == typeof(Int64))
                return "ReadInt64";

            if (t == typeof(char))
                return "ReadChar";

            if (t == typeof(char[]))
                return "ReadChars";

            if (t == typeof(UInt16))
                return "ReadUInt16";

            if (t == typeof(Int16))
                return "ReadInt16";

            else if (t == typeof(string))
                return "ReadString";

            else if (t == typeof(DateTime))
                return "ReadInt64";//ticks used in serialization

            else if (t == typeof(long))
                return "ReadInt64";

            else if (t == typeof(ulong))
                return "ReadUInt64";

            else if (t == typeof(bool))
                return "ReadBoolean";

            else if (t == typeof(byte))
                return "ReadByte";

            else if (t == typeof(sbyte))
                return "ReadSByte";

            else if (t == typeof(byte[]))
                return "ReadBytes";

            else if (t == typeof(decimal))
                return "ReadDecimal";

            else if (t == typeof(float))
                return "ReadSingle";

            else if (t == typeof(double))
                return "ReadDouble";

            else if (t == typeof(IDictionary))//currenly supports a string dictionary only
                return "ReadString";

            else
                return "";

        }

        private static MethodInfo GetBinaryWriterMethod(Type type)
        {
            MethodInfo brWrite = null;

            if (type == typeof(DateTime))
                brWrite = typeof(BinaryWriter).GetMethod("Write", new Type[] { typeof(Int64) });

            else if (type == typeof(IDictionary))//currenly supports a string dictionary only
                brWrite = typeof(BinaryWriter).GetMethod("Write", new Type[] { typeof(string) });

            else
                brWrite = typeof(BinaryWriter).GetMethod("Write", new Type[] { type });

            return brWrite;
        }

        private static TypeBuilder BuildTypeHierarchy(ModuleBuilder b, Type t)
        {
            Type baseType = t.BaseType;
            TypeBuilder eventTypeBuilder = null;

            if (baseType.Name != typeof(object).Name)
            {
                TypeBuilder tmp = BuildTypeHierarchy(b, baseType);
            }

            eventTypeBuilder = b.DefineType(t.Name, TypeAttributes.Public, baseType);

            if (!_cache.ContainsKey(t.FullName))
                _cache.Add(t.FullName, eventTypeBuilder);

            return eventTypeBuilder;
        }

        static internal bool IsIgnoreAttribute(object[] t)
        {
            foreach (Attribute attr in t)
            {
                if (attr is IgnoreDataMemberAttribute)
                {
                    return true;
                }
            }
            return false;
        }

        public static Type Generate<PARENT_TYPE>(Type type)
        {
            #region IL Initilization Code

            Type retType = null;

            try
            {
                string typeSignature = !string.IsNullOrEmpty(type.Name) ? type.Name : Unique.NewId.ToString();

                AssemblyName an = new AssemblyName(typeSignature);
                AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                    an,
                    AssemblyBuilderAccess.RunAndCollect
                );
                ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(
                    "BinarySerializableModule"
                );

                TypeBuilder serializerTypeBuilder = moduleBuilder.DefineType(type.Name + "_BinarySerializable",
                                                                    TypeAttributes.Public);

                serializerTypeBuilder.AddInterfaceImplementation(typeof(PARENT_TYPE));

                #endregion

                #region Build Type Handle Property

                FieldBuilder typeHandleFldBuilder = serializerTypeBuilder.DefineField("_typeHandle", typeof(int), FieldAttributes.Private);

                // Define the 'get_TypeHandle' method.
                MethodBuilder getTypeHandleMethod = serializerTypeBuilder.DefineMethod("GetTypeHandle",
                   MethodAttributes.Public | MethodAttributes.Virtual,
                   typeof(int), null);

                // Generate IL code for 'get_TypeHandle' method.
                ILGenerator methodIL = getTypeHandleMethod.GetILGenerator();
                methodIL.Emit(OpCodes.Ldarg_0);
                methodIL.Emit(OpCodes.Ldfld, typeHandleFldBuilder);
                methodIL.Emit(OpCodes.Ret);
                //typeHandlePropertyBuilder.SetGetMethod(getTypeHandleMethod);

                // Define the set_TypeHandle method.
                Type[] methodArgs = { typeof(int) };
                MethodBuilder setTypeHandleMethod = serializerTypeBuilder.DefineMethod("SetTypeHandle",
                   MethodAttributes.Public | MethodAttributes.Virtual,
                   typeof(void), methodArgs);
                // Generate IL code for set_TypeHandle method.
                methodIL = setTypeHandleMethod.GetILGenerator();
                methodIL.Emit(OpCodes.Ldarg_0);
                methodIL.Emit(OpCodes.Ldarg_1);
                methodIL.Emit(OpCodes.Stfld, typeHandleFldBuilder);
                methodIL.Emit(OpCodes.Ret);
                //typeHandlePropertyBuilder.SetSetMethod(setTypeHandleMethod);


                #endregion

                #region Serialize Method Builder

                Type[] dpParams = new Type[] { typeof(BinaryWriter), typeof(object) };
                MethodBuilder serializeMethod = serializerTypeBuilder.DefineMethod(
                                                       "Serialize",
                                                        MethodAttributes.Public | MethodAttributes.Virtual,
                                                        typeof(void),
                                                        dpParams);

                MethodInfo dateTimeTicks = typeof(DateTime).GetMethod("get_Ticks");
                MethodInfo convertToStringMI = typeof(Convert).GetMethod("ToString", new Type[] { typeof(int) });

                ILGenerator mthdIL = serializeMethod.GetILGenerator();
                LocalBuilder tpmEvent = mthdIL.DeclareLocal(type);
                Label labelFinally = mthdIL.DefineLabel();


                mthdIL.Emit(OpCodes.Nop);
                mthdIL.Emit(OpCodes.Ldarg_2);//PU
                mthdIL.Emit(OpCodes.Castclass, type);//PU
                mthdIL.Emit(OpCodes.Stloc, tpmEvent);//PP

                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.PropertyType == typeof(string[]))
                        continue;
                    if (IsIgnoreAttribute(pi.GetCustomAttributes(true)))
                        continue;

                    MethodInfo mi = type.GetMethod("get_" + pi.Name);
                    MethodInfo brWrite = GetBinaryWriterMethod(pi.PropertyType);

                    mthdIL.Emit(OpCodes.Ldarg_1);//PU binary writer
                    mthdIL.Emit(OpCodes.Ldloc, tpmEvent);//PU load the event object
                    mthdIL.EmitCall(OpCodes.Callvirt, mi, null);//PU get the value of the proprty

                    #region OPCodes for DateTime serialization

                    if (pi.PropertyType == typeof(DateTime))
                    {
                        LocalBuilder tmpTicks = mthdIL.DeclareLocal(typeof(DateTime));
                        mthdIL.Emit(OpCodes.Stloc, tmpTicks);
                        mthdIL.Emit(OpCodes.Ldloca_S, tmpTicks);
                        mthdIL.EmitCall(OpCodes.Call, dateTimeTicks, null);//PU
                        mthdIL.EmitCall(OpCodes.Callvirt, brWrite, null);//PU
                    }
                    #endregion

                    #region OPCodes for IDictionary serialization

                    else if (pi.PropertyType == typeof(IDictionary))
                    {

                        Label loopLabelBegin = mthdIL.DefineLabel();
                        Label loopLabelEnd = mthdIL.DefineLabel();
                        Label endFinally = mthdIL.DefineLabel();

                        LocalBuilder dictionaryEntry = mthdIL.DeclareLocal(typeof(DictionaryEntry));
                        LocalBuilder dicEnumerator = mthdIL.DeclareLocal(typeof(IDictionaryEnumerator));
                        LocalBuilder comparsionResult = mthdIL.DeclareLocal(typeof(bool));
                        LocalBuilder locIDisposable = mthdIL.DeclareLocal(typeof(IDisposable));

                        MethodInfo toString = typeof(object).GetMethod("ToString", new Type[0]);
                        MethodInfo getEnumerator = typeof(IDictionary).GetMethod("GetEnumerator", new Type[0]);
                        MethodInfo moveNext = typeof(IEnumerator).GetMethod("MoveNext", new Type[0]);
                        MethodInfo getCurrent = typeof(IEnumerator).GetMethod("get_Current", new Type[0]);
                        MethodInfo dispose = typeof(IDisposable).GetMethod("Dispose", new Type[0]);
                        MethodInfo get_Key = typeof(DictionaryEntry).GetMethod("get_Key", new Type[0]);
                        MethodInfo get_Value = typeof(DictionaryEntry).GetMethod("get_Value", new Type[0]);
                        MethodInfo get_Count = typeof(ICollection).GetMethod("get_Count");
                        MethodInfo brWriteInt = GetBinaryWriterMethod(typeof(int));

                        mthdIL.EmitCall(OpCodes.Callvirt, get_Count, null);// get the array count
                        mthdIL.EmitCall(OpCodes.Callvirt, brWriteInt, null);// write the count
                        mthdIL.Emit(OpCodes.Nop);
                        mthdIL.Emit(OpCodes.Nop);


                        mthdIL.Emit(OpCodes.Ldloc, tpmEvent);//PU load the event object
                        mthdIL.EmitCall(OpCodes.Callvirt, mi, null);//PU load  the  proprety again  into ES
                        mthdIL.EmitCall(OpCodes.Callvirt, getEnumerator, null);// get the enumerator
                        mthdIL.Emit(OpCodes.Stloc, dicEnumerator);//save the enumerator

                        mthdIL.BeginExceptionBlock();

                        mthdIL.Emit(OpCodes.Br, loopLabelEnd);// start the loop

                        mthdIL.MarkLabel(loopLabelBegin);//begin for each loop

                        mthdIL.Emit(OpCodes.Ldloc, dicEnumerator);//PU load the enumerator
                        mthdIL.EmitCall(OpCodes.Callvirt, getCurrent, null);// call get_Current
                        mthdIL.Emit(OpCodes.Unbox_Any, typeof(DictionaryEntry));
                        mthdIL.Emit(OpCodes.Stloc, dictionaryEntry);// save the DictionaryEntry

                        //get key
                        mthdIL.Emit(OpCodes.Nop);
                        mthdIL.Emit(OpCodes.Ldarg_1);//PU binary writer
                        mthdIL.Emit(OpCodes.Ldloca_S, dictionaryEntry);//load the DictionaryEntry
                        mthdIL.EmitCall(OpCodes.Call, get_Key, null);// call get_Key
                        mthdIL.EmitCall(OpCodes.Callvirt, toString, null);// call toString
                        mthdIL.EmitCall(OpCodes.Callvirt, brWrite, null);//PU call binary writer write

                        //get value
                        mthdIL.Emit(OpCodes.Nop);
                        mthdIL.Emit(OpCodes.Ldarg_1);//PU binary writer
                        mthdIL.Emit(OpCodes.Ldloca_S, dictionaryEntry);//load the DictionaryEntry
                        mthdIL.EmitCall(OpCodes.Call, get_Value, null);// call get_Value
                        mthdIL.EmitCall(OpCodes.Callvirt, toString, null);// call toString
                        mthdIL.EmitCall(OpCodes.Callvirt, brWrite, null);//PU call binary writer write

                        mthdIL.Emit(OpCodes.Nop);
                        mthdIL.Emit(OpCodes.Nop);

                        mthdIL.MarkLabel(loopLabelEnd);//end for each loop
                        mthdIL.Emit(OpCodes.Ldloc, dicEnumerator);//PU load the enumerator
                        mthdIL.EmitCall(OpCodes.Callvirt, moveNext, null);// call move next
                        mthdIL.Emit(OpCodes.Stloc, comparsionResult);//save the result
                        mthdIL.Emit(OpCodes.Ldloc, comparsionResult);//load the result
                        mthdIL.Emit(OpCodes.Brtrue_S, loopLabelBegin);//loop if true
                        //mthdIL.Emit(OpCodes.Leave_S, labelFinally);//leave if false

                        mthdIL.BeginFinallyBlock();

                        mthdIL.Emit(OpCodes.Ldloc, dicEnumerator);//PU load the enumerator
                        mthdIL.Emit(OpCodes.Isinst, typeof(System.IDisposable));
                        mthdIL.Emit(OpCodes.Stloc_S, locIDisposable);
                        mthdIL.Emit(OpCodes.Ldloc_S, locIDisposable);
                        mthdIL.Emit(OpCodes.Ldnull);
                        mthdIL.Emit(OpCodes.Ceq);
                        mthdIL.Emit(OpCodes.Stloc, comparsionResult);
                        mthdIL.Emit(OpCodes.Ldloc, comparsionResult);
                        mthdIL.Emit(OpCodes.Brtrue_S, endFinally);
                        mthdIL.Emit(OpCodes.Ldloc_S, locIDisposable);//load IDisposable
                        mthdIL.EmitCall(OpCodes.Callvirt, dispose, null);// call IDisposable::Dispose
                        mthdIL.Emit(OpCodes.Nop);

                        mthdIL.MarkLabel(endFinally);
                        mthdIL.EndExceptionBlock();

                    }

                    #endregion

                    #region OPCodes for all other type serialization

                    else
                        mthdIL.EmitCall(OpCodes.Callvirt, brWrite, null);//PU

                    #endregion

                    mthdIL.Emit(OpCodes.Nop);
                }


                Dictionary<string, FieldInfo> fldMap = new Dictionary<string, FieldInfo>();

                foreach (FieldInfo fi in type.GetFields())
                {
                    if (fi.FieldType == typeof(string[]))
                        continue;
                    if (IsIgnoreAttribute(fi.GetCustomAttributes(true)))
                        continue;

                    MethodInfo brWrite = GetBinaryWriterMethod(fi.FieldType);

                    //FieldBuilder fld = eventTypeBuilder.DefineField(fi.Name, fi.FieldType, fi.Attributes);
                    //TypeBuilder bb = declaringTypeMap[fi.DeclaringType.FullName];
                    //FieldInfo fld = null;
                    FieldInfo fld = fi;// bb.GetField(fi.Name);


                    fldMap[fi.Name] = fld;

                    mthdIL.Emit(OpCodes.Ldarg_1);//PU binary writer
                    mthdIL.Emit(OpCodes.Ldloc, tpmEvent);//PU binary writer

                    #region OPCodes for DateTime serialization

                    if (fi.FieldType == typeof(DateTime))
                    {
                        mthdIL.Emit(OpCodes.Ldflda, fld);
                        mthdIL.EmitCall(OpCodes.Call, dateTimeTicks, null);//PU
                        mthdIL.EmitCall(OpCodes.Callvirt, brWrite, null);//PU
                    }

                    #endregion

                    #region  OPCodes for IDictionary serialization

                    else if (fi.FieldType == typeof(IDictionary))
                    {

                        Label loopLabelBegin = mthdIL.DefineLabel();
                        Label loopLabelEnd = mthdIL.DefineLabel();
                        Label endFinally = mthdIL.DefineLabel();

                        LocalBuilder dictionaryEntry = mthdIL.DeclareLocal(typeof(DictionaryEntry));
                        LocalBuilder dicEnumerator = mthdIL.DeclareLocal(typeof(IDictionaryEnumerator));
                        LocalBuilder comparsionResult = mthdIL.DeclareLocal(typeof(bool));
                        LocalBuilder locIDisposable = mthdIL.DeclareLocal(typeof(IDisposable));

                        MethodInfo toString = typeof(object).GetMethod("ToString", new Type[0]);
                        MethodInfo getEnumerator = typeof(IDictionary).GetMethod("GetEnumerator", new Type[0]);
                        MethodInfo moveNext = typeof(IEnumerator).GetMethod("MoveNext", new Type[0]);
                        MethodInfo getCurrent = typeof(IEnumerator).GetMethod("get_Current", new Type[0]);
                        MethodInfo dispose = typeof(IDisposable).GetMethod("Dispose", new Type[0]);
                        MethodInfo get_Key = typeof(DictionaryEntry).GetMethod("get_Key", new Type[0]);
                        MethodInfo get_Value = typeof(DictionaryEntry).GetMethod("get_Value", new Type[0]);
                        MethodInfo get_Count = typeof(ICollection).GetMethod("get_Count");
                        MethodInfo brWriteInt = GetBinaryWriterMethod(typeof(int));

                        mthdIL.Emit(OpCodes.Ldfld, fld);
                        mthdIL.EmitCall(OpCodes.Callvirt, get_Count, null);// get the array count
                        mthdIL.EmitCall(OpCodes.Callvirt, brWriteInt, null);// write the count
                        mthdIL.Emit(OpCodes.Nop);
                        mthdIL.Emit(OpCodes.Nop);


                        mthdIL.Emit(OpCodes.Ldloc, tpmEvent);//PU load the event object
                        //mthdIL.EmitCall(OpCodes.Callvirt, mi, null);//PU load  the  proprety again  into ES
                        mthdIL.Emit(OpCodes.Ldfld, fld);
                        mthdIL.EmitCall(OpCodes.Callvirt, getEnumerator, null);// get the enumerator
                        mthdIL.Emit(OpCodes.Stloc, dicEnumerator);//save the enumerator

                        mthdIL.BeginExceptionBlock();

                        mthdIL.Emit(OpCodes.Br, loopLabelEnd);// start the loop

                        mthdIL.MarkLabel(loopLabelBegin);//begin for each loop

                        mthdIL.Emit(OpCodes.Ldloc, dicEnumerator);//PU load the enumerator
                        mthdIL.EmitCall(OpCodes.Callvirt, getCurrent, null);// call get_Current
                        mthdIL.Emit(OpCodes.Unbox_Any, typeof(DictionaryEntry));
                        mthdIL.Emit(OpCodes.Stloc, dictionaryEntry);// save the DictionaryEntry

                        //get key
                        mthdIL.Emit(OpCodes.Nop);
                        mthdIL.Emit(OpCodes.Ldarg_1);//PU binary writer
                        mthdIL.Emit(OpCodes.Ldloca_S, dictionaryEntry);//load the DictionaryEntry
                        mthdIL.EmitCall(OpCodes.Call, get_Key, null);// call get_Key
                        mthdIL.EmitCall(OpCodes.Callvirt, toString, null);// call toString
                        mthdIL.EmitCall(OpCodes.Callvirt, brWrite, null);//PU call binary writer write

                        //get value
                        mthdIL.Emit(OpCodes.Nop);
                        mthdIL.Emit(OpCodes.Ldarg_1);//PU binary writer
                        mthdIL.Emit(OpCodes.Ldloca_S, dictionaryEntry);//load the DictionaryEntry
                        mthdIL.EmitCall(OpCodes.Call, get_Value, null);// call get_Value
                        mthdIL.EmitCall(OpCodes.Callvirt, toString, null);// call toString
                        mthdIL.EmitCall(OpCodes.Callvirt, brWrite, null);//PU call binary writer write

                        mthdIL.Emit(OpCodes.Nop);
                        mthdIL.Emit(OpCodes.Nop);

                        mthdIL.MarkLabel(loopLabelEnd);//end for each loop
                        mthdIL.Emit(OpCodes.Ldloc, dicEnumerator);//PU load the enumerator
                        mthdIL.EmitCall(OpCodes.Callvirt, moveNext, null);// call move next
                        mthdIL.Emit(OpCodes.Stloc, comparsionResult);//save the result
                        mthdIL.Emit(OpCodes.Ldloc, comparsionResult);//load the result
                        mthdIL.Emit(OpCodes.Brtrue_S, loopLabelBegin);//loop if true
                        //mthdIL.Emit(OpCodes.Leave_S, labelFinally);//leave if false

                        mthdIL.BeginFinallyBlock();

                        mthdIL.Emit(OpCodes.Ldloc, dicEnumerator);//PU load the enumerator
                        mthdIL.Emit(OpCodes.Isinst, typeof(System.IDisposable));
                        mthdIL.Emit(OpCodes.Stloc_S, locIDisposable);
                        mthdIL.Emit(OpCodes.Ldloc_S, locIDisposable);
                        mthdIL.Emit(OpCodes.Ldnull);
                        mthdIL.Emit(OpCodes.Ceq);
                        mthdIL.Emit(OpCodes.Stloc, comparsionResult);
                        mthdIL.Emit(OpCodes.Ldloc, comparsionResult);
                        mthdIL.Emit(OpCodes.Brtrue_S, endFinally);
                        mthdIL.Emit(OpCodes.Ldloc_S, locIDisposable);//load IDisposable
                        mthdIL.EmitCall(OpCodes.Callvirt, dispose, null);// call IDisposable::Dispose
                        mthdIL.Emit(OpCodes.Nop);

                        mthdIL.MarkLabel(endFinally);
                        mthdIL.EndExceptionBlock();

                    }

                    #endregion

                    #region OPCodes for other types serialization
                    else
                    {
                        mthdIL.Emit(OpCodes.Ldfld, fld);
                        mthdIL.EmitCall(OpCodes.Callvirt, brWrite, null);//PU
                    }
                    #endregion

                    mthdIL.Emit(OpCodes.Nop);
                }

                mthdIL.MarkLabel(labelFinally);
                mthdIL.Emit(OpCodes.Ret);
                mthdIL = null;

                #endregion

                #region Deserialize Method Builder

                dpParams = new Type[] { typeof(BinaryReader) };
                MethodBuilder deserializeMthd = serializerTypeBuilder.DefineMethod(
                                                       "DeSerialize",
                                                        MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.Final | MethodAttributes.NewSlot,
                                                        typeof(object),
                                                        dpParams);

                ILGenerator deserializeIL = deserializeMthd.GetILGenerator();
                LocalBuilder tpmRetEvent = deserializeIL.DeclareLocal(type);
                LocalBuilder tpmRetEvent2 = deserializeIL.DeclareLocal(type);

                MethodInfo readString = typeof(BinaryReader).GetMethod("ReadString");
                MethodInfo readInt = typeof(BinaryReader).GetMethod("ReadInt32");



                Label ret = deserializeIL.DefineLabel();

                ConstructorInfo ctorEvent = type.GetConstructor(new Type[0]);
                if (ctorEvent == null)
                    throw new Exception("The event class is missing a default constructor with 0 params");

                deserializeIL.Emit(OpCodes.Newobj, ctorEvent);
                deserializeIL.Emit(OpCodes.Stloc, tpmRetEvent);

                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.PropertyType == typeof(string[]))
                        continue;

                    MethodInfo brRead = typeof(BinaryReader).GetMethod(GetBinaryReaderMethod(pi.PropertyType));
                    MethodInfo setProp = type.GetMethod("set_" + pi.Name);
                    MethodInfo getProp = type.GetMethod("get_" + pi.Name);

                    #region OPCodes for DateTime DeSerialization
                    if (pi.PropertyType == typeof(DateTime))
                    {
                        deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent);//load new obj on ES
                        deserializeIL.Emit(OpCodes.Ldarg_1);//PU binary reader ,load BR on ES
                        deserializeIL.EmitCall(OpCodes.Callvirt, brRead, null);//PU

                        FieldBuilder dateTimeFld = serializerTypeBuilder.DefineField("Ticks ", typeof(Int64), FieldAttributes.Public);
                        ConstructorInfo ctorDtTime = typeof(DateTime).GetConstructor(new Type[] { typeof(Int64) });
                        deserializeIL.Emit(OpCodes.Newobj, ctorDtTime);//PU
                        deserializeIL.EmitCall(OpCodes.Callvirt, setProp, null);//PU

                        deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent);
                        deserializeIL.Emit(OpCodes.Stloc, tpmRetEvent2);

                    }
                    #endregion

                    #region OPCodes for IDictionary DeSerialization

                    else if (pi.PropertyType == typeof(IDictionary))
                    {
                        Label loopLabelBegin = deserializeIL.DefineLabel();
                        Label loopLabelEnd = deserializeIL.DefineLabel();

                        LocalBuilder count = deserializeIL.DeclareLocal(typeof(Int32));
                        LocalBuilder key = deserializeIL.DeclareLocal(typeof(string));
                        LocalBuilder value = deserializeIL.DeclareLocal(typeof(string));
                        LocalBuilder boolVal = deserializeIL.DeclareLocal(typeof(bool));

                        MethodInfo toString = typeof(object).GetMethod("ToString", new Type[0]);
                        MethodInfo dicAdd = typeof(IDictionary).GetMethod("Add", new Type[] { typeof(object), typeof(object) });
                        MethodInfo brReadInt = typeof(BinaryReader).GetMethod(GetBinaryReaderMethod(typeof(int)));

                        deserializeIL.Emit(OpCodes.Ldarg_1);//PU binary reader ,load BR on ES
                        deserializeIL.EmitCall(OpCodes.Callvirt, brReadInt, null);//PU
                        deserializeIL.Emit(OpCodes.Stloc, count);

                        deserializeIL.Emit(OpCodes.Br, loopLabelEnd);

                        deserializeIL.MarkLabel(loopLabelBegin); //begin loop 
                        deserializeIL.Emit(OpCodes.Nop);
                        deserializeIL.Emit(OpCodes.Ldarg_1);
                        deserializeIL.EmitCall(OpCodes.Callvirt, brRead, null);
                        deserializeIL.Emit(OpCodes.Stloc, key);
                        deserializeIL.Emit(OpCodes.Ldarg_1);
                        deserializeIL.EmitCall(OpCodes.Callvirt, brRead, null);
                        deserializeIL.Emit(OpCodes.Stloc, value);

                        deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent);
                        deserializeIL.EmitCall(OpCodes.Callvirt, getProp, null);
                        deserializeIL.Emit(OpCodes.Ldloc, key);
                        deserializeIL.Emit(OpCodes.Ldloc, value);
                        deserializeIL.EmitCall(OpCodes.Callvirt, dicAdd, null);//call add method
                        deserializeIL.Emit(OpCodes.Nop);

                        deserializeIL.Emit(OpCodes.Ldloc, count);
                        deserializeIL.Emit(OpCodes.Ldc_I4_1);
                        deserializeIL.Emit(OpCodes.Sub);
                        deserializeIL.Emit(OpCodes.Stloc, count);

                        deserializeIL.MarkLabel(loopLabelEnd); //end loop 
                        deserializeIL.Emit(OpCodes.Nop);
                        deserializeIL.Emit(OpCodes.Ldloc, count);
                        deserializeIL.Emit(OpCodes.Ldc_I4_0);
                        deserializeIL.Emit(OpCodes.Ceq);
                        deserializeIL.Emit(OpCodes.Ldc_I4_0);
                        deserializeIL.Emit(OpCodes.Ceq);
                        deserializeIL.Emit(OpCodes.Stloc_S, boolVal);
                        deserializeIL.Emit(OpCodes.Ldloc_S, boolVal);
                        deserializeIL.Emit(OpCodes.Brtrue_S, loopLabelBegin);

                        deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent);
                        deserializeIL.Emit(OpCodes.Stloc, tpmRetEvent2);


                    }
                    #endregion

                    #region OPCodes for other types DeSerialization
                    else
                    {
                        deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent);//load new obj on ES
                        deserializeIL.Emit(OpCodes.Ldarg_1);//PU binary reader ,load BR on ES
                        deserializeIL.EmitCall(OpCodes.Callvirt, brRead, null);//PU

                        deserializeIL.EmitCall(OpCodes.Callvirt, setProp, null);//PU


                        deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent);
                        deserializeIL.Emit(OpCodes.Stloc, tpmRetEvent2);

                    }
                    #endregion
                }

                foreach (FieldInfo fi in type.GetFields())
                {
                    LocalBuilder locTyp = deserializeIL.DeclareLocal(fi.FieldType);
                    MethodInfo brRead = typeof(BinaryReader).GetMethod(GetBinaryReaderMethod(fi.FieldType));
                    FieldInfo fld = fldMap[fi.Name];



                    #region OPCodes for DateTime DeSerialization

                    if (fi.FieldType == typeof(DateTime))
                    {
                        ConstructorInfo ctorDtTime = typeof(DateTime).GetConstructor(new Type[] { typeof(Int64) });

                        deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent);//load new obj on ES
                        deserializeIL.Emit(OpCodes.Ldarg_1);//PU binary reader ,load BR on ES

                        deserializeIL.EmitCall(OpCodes.Callvirt, brRead, null);//PU
                        deserializeIL.Emit(OpCodes.Newobj, ctorDtTime);//PU
                        deserializeIL.Emit(OpCodes.Stfld, fld);//PU
                        deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent);
                        deserializeIL.Emit(OpCodes.Stloc, tpmRetEvent2);

                    }

                    #endregion

                    #region OPCodes for IDictionary DeSerialization

                    else if (fi.FieldType == typeof(IDictionary))
                    {
                        Label loopLabelBegin = deserializeIL.DefineLabel();
                        Label loopLabelEnd = deserializeIL.DefineLabel();

                        LocalBuilder count = deserializeIL.DeclareLocal(typeof(Int32));
                        LocalBuilder key = deserializeIL.DeclareLocal(typeof(string));
                        LocalBuilder value = deserializeIL.DeclareLocal(typeof(string));
                        LocalBuilder boolVal = deserializeIL.DeclareLocal(typeof(bool));

                        MethodInfo toString = typeof(object).GetMethod("ToString", new Type[0]);
                        MethodInfo dicAdd = typeof(IDictionary).GetMethod("Add", new Type[] { typeof(object), typeof(object) });
                        MethodInfo brReadInt = typeof(BinaryReader).GetMethod(GetBinaryReaderMethod(typeof(int)));

                        deserializeIL.Emit(OpCodes.Ldarg_1);//PU binary reader ,load BR on ES
                        deserializeIL.EmitCall(OpCodes.Callvirt, brReadInt, null);//PU
                        deserializeIL.Emit(OpCodes.Stloc, count);

                        deserializeIL.Emit(OpCodes.Br, loopLabelEnd);

                        deserializeIL.MarkLabel(loopLabelBegin); //begin loop 
                        deserializeIL.Emit(OpCodes.Nop);
                        deserializeIL.Emit(OpCodes.Ldarg_1);
                        deserializeIL.EmitCall(OpCodes.Callvirt, brRead, null);
                        deserializeIL.Emit(OpCodes.Stloc, key);
                        deserializeIL.Emit(OpCodes.Ldarg_1);
                        deserializeIL.EmitCall(OpCodes.Callvirt, brRead, null);
                        deserializeIL.Emit(OpCodes.Stloc, value);

                        deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent);
                        //deserializeIL.EmitCall(OpCodes.Callvirt, getProp, null);
                        deserializeIL.Emit(OpCodes.Ldfld, fld);
                        deserializeIL.Emit(OpCodes.Ldloc, key);
                        deserializeIL.Emit(OpCodes.Ldloc, value);
                        deserializeIL.EmitCall(OpCodes.Callvirt, dicAdd, null);//call add method
                        deserializeIL.Emit(OpCodes.Nop);

                        deserializeIL.Emit(OpCodes.Ldloc, count);
                        deserializeIL.Emit(OpCodes.Ldc_I4_1);
                        deserializeIL.Emit(OpCodes.Sub);
                        deserializeIL.Emit(OpCodes.Stloc, count);

                        deserializeIL.MarkLabel(loopLabelEnd); //end loop 
                        deserializeIL.Emit(OpCodes.Nop);
                        deserializeIL.Emit(OpCodes.Ldloc, count);
                        deserializeIL.Emit(OpCodes.Ldc_I4_0);
                        deserializeIL.Emit(OpCodes.Ceq);
                        deserializeIL.Emit(OpCodes.Ldc_I4_0);
                        deserializeIL.Emit(OpCodes.Ceq);
                        deserializeIL.Emit(OpCodes.Stloc_S, boolVal);
                        deserializeIL.Emit(OpCodes.Ldloc_S, boolVal);
                        deserializeIL.Emit(OpCodes.Brtrue_S, loopLabelBegin);

                        deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent);
                        deserializeIL.Emit(OpCodes.Stloc, tpmRetEvent2);
                    }
                    #endregion

                    #region OPCodes for other types DeSerialization

                    else
                    {
                        deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent);//load new obj on ES
                        deserializeIL.Emit(OpCodes.Ldarg_1);//PU binary reader ,load BR on ES

                        deserializeIL.EmitCall(OpCodes.Callvirt, brRead, null);//PU
                        deserializeIL.Emit(OpCodes.Stfld, fld);//PU
                        deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent);
                        deserializeIL.Emit(OpCodes.Stloc, tpmRetEvent2);
                    }

                    #endregion

                }


                deserializeIL.Emit(OpCodes.Br_S, ret);
                deserializeIL.MarkLabel(ret);
                deserializeIL.Emit(OpCodes.Ldloc, tpmRetEvent2);
                deserializeIL.Emit(OpCodes.Ret);

                #endregion


                retType = serializerTypeBuilder.CreateType();
                //eventTypeBuilder.CreateType();
                //foreach (KeyValuePair<string, TypeBuilder> d in declaringTypeMap)
                // {
                //d.Value.CreateType();
                //}
                //myAsmBuilder.Save(myAsmBuilder.GetName().Name + ".dll");
            }
            catch (Exception x)
            {
                String s = x.Message;
                retType = null;
            }



            return retType;

        }
    }
}