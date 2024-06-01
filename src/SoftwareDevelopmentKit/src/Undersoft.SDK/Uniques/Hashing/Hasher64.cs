namespace Undersoft.SDK.Uniques.Hashing
{
    using Algorithm;

    public static class Hasher64
    {
        public static unsafe byte[] ComputeBytes(byte* bytes, int length, long seed = 0)
        {
            byte[] b = new byte[8];
            fixed (byte* pb = b)
            {
                *(ulong*)pb = xxHash64.UnsafeComputeHash(bytes, length, (ulong)seed) << 12 >> 12;
            }
            return b;
        }

        public static unsafe byte[] ComputeBytes(byte[] bytes, long seed = 0)
        {
            byte[] b = new byte[8];
            fixed (
                byte* pb = b,
                    pa = bytes
            )
            {
                *(ulong*)pb = xxHash64.UnsafeComputeHash(pa, bytes.Length, (ulong)seed) << 12 >> 12;
            }
            return b;
        }

        public static unsafe ulong ComputeKey(byte* ptr, int length, long seed = 0)
        {
            return xxHash64.UnsafeComputeHash(ptr, length, (ulong)seed) << 12 >> 12;
        }

        public static unsafe ulong ComputeKey(byte[] bytes, long seed = 0)
        {
            fixed (byte* pa = bytes)
            {
                return xxHash64.UnsafeComputeHash(pa, bytes.Length, (ulong)seed) << 12 >> 12;
            }
        }
    }
}
