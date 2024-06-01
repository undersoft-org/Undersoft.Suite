namespace System
{
    public static class Int64Extension
    {
        public static bool IsEven(this long i)
        {
            return !((i & 1L) != 0);
        }

        public static bool IsOdd(this long i)
        {
            return ((i & 1) != 0);
        }

        public static long RemoveSign(this long i)
        {
            return (long)(((ulong)i << 1) >> 1);
        }
    }
}
