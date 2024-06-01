using System.Collections;
using System.Runtime.InteropServices;

namespace Undersoft.SDK.Extracting
{
    using Compiler;

    public static partial class Extract
    {
        private static IExtract extractor = ExtractCompiler.GetExtractor();

        public static IExtract Perform => extractor;

        public unsafe static bool BlockEqual(
            byte* source,
            long srcOffset,
            byte* dest,
            long destOffset,
            long count
        )
        {
            long sl = count;
            long sl64 = sl / 8;
            long sl8 = sl % 8;
            long* lsrc = (long*)(source + srcOffset),
                ldst = (long*)(dest + destOffset);
            for (int i = 0; i < sl64; i++)
                if (*(&lsrc[i]) != *(&ldst[i]))
                    return false;
            byte* psrc8 = source + (sl64 * 8),
                pdst8 = dest + (sl64 * 8);
            for (int i = 0; i < sl8; i++)
                if (*(&psrc8[i]) != *(&pdst8[i]))
                    return false;
            return true;
        }



        public unsafe static bool BlockEqual(byte[] source, byte[] dest)
        {
            long sl = source.LongLength;
            if (sl > dest.LongLength)
                return false;
            long sl64 = sl / 8;
            long sl8 = sl % 8;
            fixed (
                byte* psrc = source,
                    pdst = dest
            )
            {
                long* lsrc = (long*)psrc,
                    ldst = (long*)pdst;
                for (int i = 0; i < sl64; i++)
                    if (*(&lsrc[i]) != (*(&ldst[i])))
                        return false;
                byte* psrc8 = psrc + (sl64 * 8),
                    pdst8 = pdst + (sl64 * 8);
                for (int i = 0; i < sl8; i++)
                    if (*(&psrc8[i]) != (*(&pdst8[i])))
                        return false;
                return true;
            }
        }

        public unsafe static object BytesToStructure(byte[] binary, object structure, long offset)
        {
            if (structure is ValueType)
            {
                return ExtractCompiler.BytesToValueStructure(binary, structure, 0);
            }
            else
            {
                fixed (byte* b = &binary[offset])
                    return PointerToStructure(new IntPtr(b), structure);
            }
        }

        public unsafe static ValueType BytesToStructure(
            byte[] binary,
            ref ValueType structure,
            long offset
        )
        {
            extractor.BytesToValueStructure(binary, ref structure, 0);
            return structure;
        }

        public static unsafe object BytesToStructure(byte[] binary, Type structure, long offset)
        {
            fixed (byte* b = &binary[offset])
                return PointerToStructure(new IntPtr(b), structure, 0);
        }

        public unsafe static int GetSize(object structure)
        {
            if (structure is ValueType)
                return ValueTypeObjectSize(structure);
            if (structure.GetType().IsLayoutSequential)
                return Marshal.SizeOf(structure);
            if (structure is String || structure is IFormattable)
                return structure.ToString().Length * sizeof(char);
            if (structure is IList)
                return GetSizes(((IList)structure)).Sum();
            return 0;
        }

        public unsafe static int[] GetSizes(IList list)
        {
            int c = list.Count;
            if (c > 0)
            {
                if (list.GetType().HasElementType)
                {
                    var e = list.GetType().GetElementType();
                    if (e.IsValueType)
                        return new int[] { ValueTypeItemSize(e) * c };
                    if (e == typeof(string))
                        return list.Cast<string>().Select(p => p.Length).ToArray();
                    if (e.IsLayoutSequential)
                        return new int[c].Select(l => l = Marshal.SizeOf(e)).ToArray();
                    if (e.IsArray)
                    {
                        return list.Cast<object>().Select(a => GetSizes(a).Sum()).ToArray();
                    }
                }
                return list.Cast<object>().Select(o => o.GetSize()).ToArray();
            }
            return new int[0];
        }

        public unsafe static int[] GetSizes(object structure)
        {
            if (structure is ValueType)
                return new int[] { ValueTypeObjectSize(structure) };
            if (structure is not String && structure is not IFormattable)
                return new int[] { structure.ToString().Length };
            if (structure.GetType().IsLayoutSequential)
                return new int[] { Marshal.SizeOf(structure) };
            if (structure is IList)
                return GetSizes(((IList)structure));

            return new int[0];
        }

        public unsafe static int[] GetSizes(object[] array)
        {
            return GetSizes((IList)array);
        }

        public unsafe static byte[] GetStructureBytes(object structure)
        {
            byte[] b = null;

            if (structure is ValueType)
            {
                Type t = structure.GetType();
                if (t.IsPrimitive || t.IsLayoutSequential)
                    return extractor.ValueStructureToBytes(structure);

                if (structure is DateTime)
                {
                    b = new byte[8];
                    structure = ((DateTime)structure).ToBinary();
                }
                else if (structure is Enum)
                {
                    b = new byte[4];
                    structure = Convert.ToInt32((Enum)structure);
                }
                else
                {
                    b = new byte[Marshal.SizeOf(structure)];
                }
            }
            else if (structure.GetType() == typeof(string))
            {
                int l = ((string)structure).Length;
                b = new byte[l];
                fixed (char* c = (string)structure)
                fixed (byte* pb = b)
                    CopyBlock(pb, (byte*)c, l);
                return b;
            }
            else
                b = new byte[Marshal.SizeOf(structure)];

            fixed (byte* pb = b)
                Marshal.StructureToPtr(structure, new IntPtr(pb), false);
            return b;
        }

        public unsafe static byte[] GetStructureBytes(ValueType structure)
        {
            Type t = structure.GetType();
            if (t.IsPrimitive || t.IsLayoutSequential)
                return extractor.ValueStructureToBytes(structure);

            byte[] b = null;
            var _structure = structure;
            if (structure is DateTime)
            {
                b = new byte[8];
                _structure = ((DateTime)structure).ToBinary();
            }
            else if (structure is Enum)
            {
                b = new byte[4];
                _structure = Convert.ToInt32((Enum)structure);
            }
            else
            {
                b = new byte[Marshal.SizeOf(_structure)];
            }

            fixed (byte* pb = b)
                Marshal.StructureToPtr(_structure, new IntPtr(pb), false);
            return b;
        }

        public unsafe static IntPtr GetStructureIntPtr(object structure)
        {
            int size = 0;

            if (structure is ValueType)
            {
                Type t = structure.GetType();
                if (t.IsPrimitive)
                {
                    size = Marshal.SizeOf(structure);
                }
                else if (structure is DateTime)
                {
                    size = 8;
                    structure = ((DateTime)structure).ToBinary();
                }
                else if (structure is Enum)
                {
                    size = 4;
                    structure = Convert.ToInt32((Enum)structure);
                }
                else if (t.IsLayoutSequential)
                {
                    return new IntPtr(extractor.ValueStructureToPointer(structure));
                }
                else
                    size = Marshal.SizeOf(structure);
            }
            else
                size = Marshal.SizeOf(structure);

            IntPtr p = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structure, p, true);

            return p;
        }

        public unsafe static byte* GetStructurePointer(object structure)
        {
            IntPtr p = GetStructureIntPtr(structure);
            return (byte*)(p.ToPointer());
        }

        public unsafe static object PointerToStructure(byte* binary, object structure)
        {
            return PointerToStructure(new IntPtr(binary), structure);
        }

        public unsafe static object PointerToStructure(byte* binary, Type structure, long offset)
        {
            return PointerToStructure(new IntPtr(binary + offset), structure, 0);
        }

        public unsafe static ValueType PointerToStructure(byte* binary, ValueType structure)
        {
            return ExtractCompiler.PointerToValueStructure(binary, structure, 0);
        }

        public unsafe static object PointerToStructure(IntPtr binary, object structure)
        {
            if (structure is ValueType)
            {
                Type t = structure.GetType();
                if (t.IsLayoutSequential)
                {
                    return ExtractCompiler.PointerToValueStructure(
                        (byte*)(binary.ToPointer()),
                        structure,
                        0
                    );
                }
                else
                    return PointerToStructure(binary, structure.GetType(), 0);
            }
            else
                Marshal.PtrToStructure(binary, structure);
            return structure;
        }

        public static object PointerToStructure(IntPtr binary, Type structure, int offset)
        {
            if (structure == typeof(DateTime))
                return DateTime.FromBinary(
                    (long)Marshal.PtrToStructure(binary + (int)offset, typeof(long))
                );
            else
                return Marshal.PtrToStructure(binary, structure);
        }

        public unsafe static ValueType PointerToStructure(IntPtr binary, ValueType structure)
        {
            return ExtractCompiler.PointerToValueStructure(
                (byte*)(binary.ToPointer()),
                structure,
                0
            );
        }

        public unsafe static void StructureToBytes(object structure, ref byte[] binary, long offset)
        {
            fixed (byte* pb = &binary[offset])
            {
                IntPtr p = new IntPtr(pb);
                StructureToPointer(structure, p);
            }
        }

        public unsafe static void StructureToBytes(
            ValueType structure,
            ref byte[] binary,
            long offset
        )
        {
            fixed (byte* pb = &binary[offset])
            {
                IntPtr p = new IntPtr(pb);
                StructureToPointer(structure, p);
            }
        }

        public unsafe static void StructureToPointer(object structure, byte* binary)
        {
            IntPtr p = new IntPtr(binary);
            StructureToPointer(structure, p);
            binary = (byte*)p;
        }

        public static void StructureToPointer(object structure, IntPtr binary)
        {
            if (structure is ValueType)
            {
                ValueStructureToPointer(structure, binary);
                return;
            }

            Marshal.StructureToPtr(structure, binary, true);
        }

        public unsafe static void ValueStructureToPointer(ValueType structure, byte* binary)
        {
            IntPtr p = new IntPtr(binary);
            ValueStructureToPointer(structure, p);
        }

        public unsafe static void ValueStructureToPointer(object structure, IntPtr binary)
        {
            var t = structure.GetType();
            if (t.IsPrimitive)
            {
                Marshal.StructureToPtr(structure, binary, false);
                return;
            }
            if (structure is DateTime)
            {
                Marshal.StructureToPtr(((DateTime)structure).ToBinary(), binary, false);
                return;
            }
            if (structure is Enum)
            {
                Marshal.StructureToPtr(Convert.ToUInt32(structure), binary, false);
                return;
            }
            if (t.IsLayoutSequential)
            {
                extractor.ValueStructureToPointer(structure, (byte*)(binary.ToPointer()), 0);
                return;
            }

            Marshal.StructureToPtr(structure, binary, false);
        }

        private static int ValueTypeItemSize(Type e)
        {
            if (e.IsPrimitive || e.IsLayoutSequential)
                return Marshal.SizeOf(e);
            if (e == typeof(DateTime))
                return 8;
            if (e == typeof(Enum))
                return 4;
            try
            {
                return Marshal.SizeOf(e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int ValueTypeObjectSize(object structure)
        {
            return ValueTypeItemSize(structure.GetType());
        }
    }
}
