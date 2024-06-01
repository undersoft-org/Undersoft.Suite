namespace Undersoft.SDK.Extracting
{
    public static class IntPtrExtractExtenstion
    {
        public static unsafe void CopyBlock(this IntPtr src, IntPtr dest, uint count)
        {
            Extract.CopyBlock(dest, 0, src, 0, count);
        }

        public static unsafe void CopyBlock(this IntPtr src, IntPtr dest, uint offset, uint count)
        {
            Extract.CopyBlock(dest, offset, src, 0, count);
        }

        public static unsafe void CopyBlock(this IntPtr src, IntPtr dest, ulong count)
        {
            Extract.CopyBlock(dest, 0, src, 0, count);
        }

        public static unsafe void CopyBlock(this IntPtr src, IntPtr dest, ulong offset, ulong count)
        {
            Extract.CopyBlock(dest, offset, src, 0, count);
        }

        public static unsafe void CopyBlock(
            this IntPtr src,
            uint srcOffset,
            IntPtr dest,
            uint destOffset,
            uint count
        )
        {
            Extract.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static unsafe void CopyBlock(
            this IntPtr src,
            ulong srcOffset,
            IntPtr dest,
            ulong destOffset,
            ulong count
        )
        {
            Extract.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public unsafe static void FromStructure(this IntPtr binary, object structure)
        {
            Extract.StructureToPointer(structure, binary);
        }

        public unsafe static object NewStructure(this IntPtr binary, Type structure, int offset)
        {
            return Extract.PointerToStructure(binary, structure, offset);
        }

        public unsafe static object ToStructure(this IntPtr binary, object structure)
        {
            return Extract.PointerToStructure(binary, structure);
        }
    }
}
