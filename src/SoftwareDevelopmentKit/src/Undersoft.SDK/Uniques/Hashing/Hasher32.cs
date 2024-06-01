namespace Undersoft.SDK.Uniques.Hashing
{
    using Algorithm;

    public static class Hasher32
    {
        public static unsafe byte[] ComputeBytes(byte* ptr, int length, long seed = 0)
        {
            byte[] b = new byte[4];
            fixed (byte* pb = b)
            {
                *(int*)pb = (int)xxHash32.UnsafeComputeHash(ptr, length, (uint)seed);
            }
            return b;
        }

        public static unsafe byte[] ComputeBytes(byte[] bytes, long seed = 0)
        {
            byte[] b = new byte[4];
            fixed (
                byte* pb = b,
                    pa = bytes
            )
            {
                *(int*)pb = (int)xxHash32.UnsafeComputeHash(pa, bytes.Length, (uint)seed);
            }
            return b;
        }

        public static unsafe int ComputeKey(byte* ptr, int length, long seed = 0)
        {
            return (int)xxHash32.UnsafeComputeHash(ptr, length, (uint)seed);
        }

        public static unsafe int ComputeKey(byte[] bytes, long seed = 0)
        {
            fixed (byte* pa = bytes)
            {
                return (int)xxHash32.UnsafeComputeHash(pa, bytes.Length, (uint)seed);
            }
        }
    }
}
