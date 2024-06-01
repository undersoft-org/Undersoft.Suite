namespace System
{
    public static class UInt64Extension
    {
        public static bool IsEven(this ulong i)
        {
            return !((i & 1UL) != 0);
        }

        public static bool IsOdd(this ulong i)
        {
            return ((i & 1UL) != 0);
        }
    }
}
