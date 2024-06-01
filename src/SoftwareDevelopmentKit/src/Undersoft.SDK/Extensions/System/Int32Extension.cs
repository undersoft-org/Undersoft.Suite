namespace System
{
    public static class Int32Extension
    {
        public static bool IsEven(this int i)
        {
            return !((i & 1) != 0);
        }

        public static bool IsOdd(this int i)
        {
            return ((i & 1) != 0);
        }

        public static int RemoveSign(this int i)
        {
            return (int)(((uint)i << 1) >> 1);
        }
    }
}
