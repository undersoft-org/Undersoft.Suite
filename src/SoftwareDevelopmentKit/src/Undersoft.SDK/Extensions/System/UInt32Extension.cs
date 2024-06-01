namespace System
{
    public static class UInt32Extension
    {
        public static bool IsEven(this uint i)
        {
            return !((i & 1) != 0);
        }

        public static bool IsOdd(this uint i)
        {
            return ((i & 1) != 0);
        }
    }
}
