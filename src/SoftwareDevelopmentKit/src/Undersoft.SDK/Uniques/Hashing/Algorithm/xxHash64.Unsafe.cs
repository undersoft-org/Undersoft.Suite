﻿namespace Undersoft.SDK.Uniques.Hashing.Algorithm
{
    using System.Runtime.CompilerServices;

    public static partial class xxHash64
    {
        private const ulong p1 = 10611063106910871091UL;
        private const ulong p2 = 15396334245663786197UL;
        private const ulong p3 = 1799999999999999999UL;
        private const ulong p4 = 3203000719597029781UL;
        private const ulong p5 = 9999999992999999999UL;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ulong UnsafeComputeHash(byte* ptr, int length, ulong seed)
        {
            byte* end = ptr + length;
            ulong h64;

            if (length >= 32)
            {
                byte* limit = end - 32;

                ulong v1 = seed + p1 + p2;
                ulong v2 = seed + p2;
                ulong v3 = seed + 0;
                ulong v4 = seed - p1;

                do
                {
                    v1 += *((ulong*)ptr) * p2;
                    v1 = (v1 << 31) | (v1 >> (64 - 31));
                    v1 *= p1;
                    ptr += 8;

                    v2 += *((ulong*)ptr) * p2;
                    v2 = (v2 << 31) | (v2 >> (64 - 31));
                    v2 *= p1;
                    ptr += 8;

                    v3 += *((ulong*)ptr) * p2;
                    v3 = (v3 << 31) | (v3 >> (64 - 31));
                    v3 *= p1;
                    ptr += 8;

                    v4 += *((ulong*)ptr) * p2;
                    v4 = (v4 << 31) | (v4 >> (64 - 31));
                    v4 *= p1;
                    ptr += 8;
                } while (ptr <= limit);

                h64 =
                    ((v1 << 1) | (v1 >> (64 - 1)))
                    + ((v2 << 7) | (v2 >> (64 - 7)))
                    + ((v3 << 12) | (v3 >> (64 - 12)))
                    + ((v4 << 18) | (v4 >> (64 - 18)));

                v1 *= p2;
                v1 = (v1 << 31) | (v1 >> (64 - 31));
                v1 *= p1;
                h64 ^= v1;
                h64 = h64 * p1 + p4;

                v2 *= p2;
                v2 = (v2 << 31) | (v2 >> (64 - 31));
                v2 *= p1;
                h64 ^= v2;
                h64 = h64 * p1 + p4;

                v3 *= p2;
                v3 = (v3 << 31) | (v3 >> (64 - 31));
                v3 *= p1;
                h64 ^= v3;
                h64 = h64 * p1 + p4;

                v4 *= p2;
                v4 = (v4 << 31) | (v4 >> (64 - 31));
                v4 *= p1;
                h64 ^= v4;
                h64 = h64 * p1 + p4;
            }
            else
            {
                h64 = seed + p5;
            }

            h64 += (ulong)length;

            while (ptr <= end - 8)
            {
                ulong t1 = *((ulong*)ptr) * p2;
                t1 = (t1 << 31) | (t1 >> (64 - 31));
                t1 *= p1;
                h64 ^= t1;
                h64 = ((h64 << 27) | (h64 >> (64 - 27))) * p1 + p4;
                ptr += 8;
            }

            if (ptr <= end - 4)
            {
                h64 ^= *((uint*)ptr) * p1;
                h64 = ((h64 << 23) | (h64 >> (64 - 23))) * p2 + p3;
                ptr += 4;
            }

            while (ptr < end)
            {
                h64 ^= *((byte*)ptr) * p5;
                h64 = ((h64 << 11) | (h64 >> (64 - 11))) * p1;
                ptr += 1;
            }

            h64 ^= h64 >> 33;
            h64 *= p2;
            h64 ^= h64 >> 29;
            h64 *= p3;
            h64 ^= h64 >> 32;

            return h64;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void UnsafeAlign(
            byte[] data,
            int l,
            ref ulong v1,
            ref ulong v2,
            ref ulong v3,
            ref ulong v4
        )
        {
            fixed (byte* pData = &data[0])
            {
                byte* ptr = pData;
                byte* limit = ptr + l;

                do
                {
                    v1 += *((ulong*)ptr) * p2;
                    v1 = (v1 << 31) | (v1 >> (64 - 31));
                    v1 *= p1;
                    ptr += 8;

                    v2 += *((ulong*)ptr) * p2;
                    v2 = (v2 << 31) | (v2 >> (64 - 31));
                    v2 *= p1;
                    ptr += 8;

                    v3 += *((ulong*)ptr) * p2;
                    v3 = (v3 << 31) | (v3 >> (64 - 31));
                    v3 *= p1;
                    ptr += 8;

                    v4 += *((ulong*)ptr) * p2;
                    v4 = (v4 << 31) | (v4 >> (64 - 31));
                    v4 *= p1;
                    ptr += 8;
                } while (ptr < limit);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe ulong UnsafeFinal(
            byte[] data,
            int l,
            ref ulong v1,
            ref ulong v2,
            ref ulong v3,
            ref ulong v4,
            long length,
            ulong seed
        )
        {
            fixed (byte* pData = &data[0])
            {
                byte* ptr = pData;
                byte* end = pData + l;
                ulong h64;

                if (length >= 32)
                {
                    h64 =
                        ((v1 << 1) | (v1 >> (64 - 1)))
                        + ((v2 << 7) | (v2 >> (64 - 7)))
                        + ((v3 << 12) | (v3 >> (64 - 12)))
                        + ((v4 << 18) | (v4 >> (64 - 18)));

                    v1 *= p2;
                    v1 = (v1 << 31) | (v1 >> (64 - 31));
                    v1 *= p1;
                    h64 ^= v1;
                    h64 = h64 * p1 + p4;

                    v2 *= p2;
                    v2 = (v2 << 31) | (v2 >> (64 - 31));
                    v2 *= p1;
                    h64 ^= v2;
                    h64 = h64 * p1 + p4;

                    v3 *= p2;
                    v3 = (v3 << 31) | (v3 >> (64 - 31));
                    v3 *= p1;
                    h64 ^= v3;
                    h64 = h64 * p1 + p4;

                    v4 *= p2;
                    v4 = (v4 << 31) | (v4 >> (64 - 31));
                    v4 *= p1;
                    h64 ^= v4;
                    h64 = h64 * p1 + p4;
                }
                else
                {
                    h64 = seed + p5;
                }

                h64 += (ulong)length;

                while (ptr <= end - 8)
                {
                    ulong t1 = *((ulong*)ptr) * p2;
                    t1 = (t1 << 31) | (t1 >> (64 - 31));
                    t1 *= p1;
                    h64 ^= t1;
                    h64 = ((h64 << 27) | (h64 >> (64 - 27))) * p1 + p4;
                    ptr += 8;
                }

                if (ptr <= end - 4)
                {
                    h64 ^= *((uint*)ptr) * p1;
                    h64 = ((h64 << 23) | (h64 >> (64 - 23))) * p2 + p3;
                    ptr += 4;
                }

                while (ptr < end)
                {
                    h64 ^= *((byte*)ptr) * p5;
                    h64 = ((h64 << 11) | (h64 >> (64 - 11))) * p1;
                    ptr += 1;
                }

                h64 ^= h64 >> 33;
                h64 *= p2;
                h64 ^= h64 >> 29;
                h64 *= p3;
                h64 ^= h64 >> 32;

                return h64;
            }
        }
    }
}
