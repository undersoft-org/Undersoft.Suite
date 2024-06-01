namespace Undersoft.SDK.Extracting
{
    public static class TypeExtractExtenstion
    {
        public unsafe static object NewStructure(this Type structure, byte* binary, long offset = 0)
        {
            return Extract.PointerToStructure(binary, structure, offset);
        }

        public static object NewStructure(this Type structure, byte[] binary, long offset = 0)
        {
            return Extract.BytesToStructure(binary, structure, offset);
        }
    }
}
