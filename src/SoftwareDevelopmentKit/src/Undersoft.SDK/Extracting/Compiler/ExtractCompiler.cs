namespace Undersoft.SDK.Extracting.Compiler
{
    using System.Reflection;
    using System.Reflection.Emit;

    public static partial class ExtractCompiler
    {
        internal static readonly IExtract _extract;
        private static AssemblyBuilder _asmBuilder;
        private static AssemblyName _asmName = new AssemblyName() { Name = "Extracting" };
        private static ModuleBuilder _modBuilder;

        static ExtractCompiler()
        {
            _asmBuilder = AssemblyBuilder.DefineDynamicAssembly(
                _asmName,
                AssemblyBuilderAccess.RunAndCollect
            );
            _modBuilder = _asmBuilder.DefineDynamicModule(_asmName.Name + ".dll");

            var typeBuilder = _modBuilder.DefineType(
                "Extracting",
                TypeAttributes.Public
                    | TypeAttributes.AutoClass
                    | TypeAttributes.AnsiClass
                    | TypeAttributes.Class
                    | TypeAttributes.BeforeFieldInit
            );

            typeBuilder.AddInterfaceImplementation(typeof(IExtract));

            CompileCopyByteArrayBlockUInt32(typeBuilder);
            CompileCopyPointerBlockUInt32(typeBuilder);

            CompileCopyByteArrayBlockUInt64(typeBuilder);
            CompileCopyPointerBlockUInt64(typeBuilder);

            CompileValueObjectToPointer(typeBuilder);
            CompileValueObjectToByteArrayRef(typeBuilder);

            CompileValueObjectToNewByteArray(typeBuilder);
            CompileValueTypeToNewByteArray(typeBuilder);
            CompileValueObjectToNewPointer(typeBuilder);

            CompilePointerToValueObject(typeBuilder);
            CompilePointerToValueType(typeBuilder);

            CompileByteArrayToValueObject(typeBuilder);
            CompileByteArrayToValueType(typeBuilder);

            TypeInfo _extractType = typeBuilder.CreateTypeInfo();
            _extract = (IExtract)Activator.CreateInstance(_extractType);
        }

        public unsafe static ValueType BytesToValueStructure(
            byte[] array,
            ValueType structure,
            ulong offset
        )
        {
            _extract.BytesToValueStructure(array, ref structure, offset);
            return structure;
        }

        public unsafe static object BytesToValueStructure(
            byte[] ptr,
            object structure,
            ulong offset
        )
        {
            var _structure = structure;
            _extract.BytesToValueStructure(ptr, ref _structure, offset);
            return _structure;
        }

        public static unsafe void CopyBlock(
            byte* dest,
            uint destOffset,
            byte* src,
            uint srcOffset,
            uint count
        )
        {
            _extract.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static unsafe void CopyBlock(
            byte* dest,
            ulong destOffset,
            byte* src,
            ulong srcOffset,
            ulong count
        )
        {
            _extract.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static unsafe void CopyBlock(
            byte[] dest,
            uint destOffset,
            byte[] src,
            uint srcOffset,
            uint count
        )
        {
            _extract.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static unsafe void CopyBlock(
            byte[] dest,
            ulong destOffset,
            byte[] src,
            ulong srcOffset,
            ulong count
        )
        {
            _extract.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static IExtract GetExtractor()
        {
            return _extract;
        }

        public unsafe static object PointerToValueStructure(
            byte* ptr,
            object structure,
            ulong offset
        )
        {
            _extract.PointerToValueStructure(ptr, ref structure, offset);
            return structure;
        }

        public unsafe static ValueType PointerToValueStructure(
            byte* ptr,
            ValueType structure,
            ulong offset
        )
        {
            _extract.PointerToValueStructure(ptr, ref structure, offset);
            return structure;
        }

        public unsafe static object PointerToValueStructure(
            IntPtr ptr,
            object structure,
            ulong offset
        )
        {
            _extract.PointerToValueStructure((byte*)ptr.ToPointer(), ref structure, offset);
            return structure;
        }

        public unsafe static ValueType PointerToValueStructure(
            IntPtr ptr,
            ValueType structure,
            ulong offset
        )
        {
            _extract.PointerToValueStructure((byte*)ptr.ToPointer(), ref structure, offset);
            return structure;
        }

        public static byte[] ValueStructureToBytes(object structure)
        {
            return _extract.ValueStructureToBytes(structure);
        }

        public unsafe static void ValueStructureToBytes(
            object structure,
            ref byte[] ptr,
            ulong offset
        )
        {
            _extract.ValueStructureToBytes(structure, ref ptr, offset);
        }

        public static byte[] ValueStructureToBytes(ValueType structure)
        {
            return _extract.ValueStructureToBytes(structure);
        }

        public static unsafe IntPtr ValueStructureToIntPtr(object structure)
        {
            return new IntPtr(_extract.ValueStructureToPointer(structure));
        }

        public static unsafe byte* ValueStructureToPointer(object structure)
        {
            return _extract.ValueStructureToPointer(structure);
        }

        public unsafe static void ValueStructureToPointer(object structure, byte* ptr, ulong offset)
        {
            _extract.ValueStructureToPointer(structure, ptr, offset);
        }
    }
}
