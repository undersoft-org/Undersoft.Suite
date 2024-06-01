﻿namespace Undersoft.SDK.Extracting
{
    public static partial class Extract
    {
        public static unsafe void CopyBlock(byte* dest, byte* src, uint count)
        {
            extractor.CopyBlock(dest, 0, src, 0, count);
        }

        public static unsafe void CopyBlock(byte* dest, byte* src, uint destOffset, uint count)
        {
            extractor.CopyBlock(dest, destOffset, src, 0, count);
        }

        public static unsafe void CopyBlock(byte* dest, byte* src, ulong count)
        {
            extractor.CopyBlock(dest, 0, src, 0, count);
        }

        public static unsafe void CopyBlock(byte* dest, byte* src, ulong destOffset, ulong count)
        {
            extractor.CopyBlock(dest, destOffset, src, 0, count);
        }

        public static unsafe void CopyBlock(
            byte* dest,
            uint destOffset,
            byte* src,
            uint srcOffset,
            uint count
        )
        {
            extractor.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static unsafe void CopyBlock(
            byte* dest,
            ulong destOffset,
            byte* src,
            ulong srcOffset,
            ulong count
        )
        {
            extractor.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static unsafe void CopyBlock(byte[] dest, byte[] src, ulong count)
        {
            extractor.CopyBlock(dest, 0, src, 0, count);
        }

        public static unsafe void CopyBlock(byte[] dest, byte[] src, uint count)
        {
            extractor.CopyBlock(dest, 0, src, 0, count);
        }

        public static unsafe void CopyBlock(byte[] dest, byte[] src, uint destOffset, uint count)
        {
            extractor.CopyBlock(dest, destOffset, src, 0, count);
        }

        public static unsafe void CopyBlock(byte[] dest, byte[] src, ulong destOffset, ulong count)
        {
            extractor.CopyBlock(dest, destOffset, src, 0, count);
        }

        public static unsafe void CopyBlock(
            byte[] dest,
            uint destOffset,
            byte[] src,
            uint srcOffset,
            uint count
        )
        {
            extractor.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static unsafe void CopyBlock(
            byte[] dest,
            ulong destOffset,
            byte[] src,
            ulong srcOffset,
            ulong count
        )
        {
            extractor.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static unsafe void CopyBlock(IntPtr dest, IntPtr src, uint count)
        {
            extractor.CopyBlock((byte*)(dest.ToPointer()), 0, (byte*)(src.ToPointer()), 0, count);
        }

        public static unsafe void CopyBlock(IntPtr dest, IntPtr src, uint destOffset, uint count)
        {
            extractor.CopyBlock(
                (byte*)src.ToPointer(),
                destOffset,
                (byte*)dest.ToPointer(),
                0,
                count
            );
        }

        public static unsafe void CopyBlock(IntPtr dest, IntPtr src, ulong count)
        {
            extractor.CopyBlock((byte*)(dest.ToPointer()), 0, (byte*)(src.ToPointer()), 0, count);
        }

        public static unsafe void CopyBlock(IntPtr dest, IntPtr src, ulong destOffset, ulong count)
        {
            extractor.CopyBlock(
                (byte*)(dest.ToPointer()),
                destOffset,
                (byte*)(src.ToPointer()),
                0,
                count
            );
        }

        public static unsafe void CopyBlock(
            IntPtr dest,
            uint destOffset,
            IntPtr src,
            uint srcOffset,
            uint count
        )
        {
            extractor.CopyBlock(
                (byte*)(dest.ToPointer()),
                destOffset,
                (byte*)(src.ToPointer()),
                srcOffset,
                count
            );
        }

        public static unsafe void CopyBlock(
            IntPtr dest,
            ulong destOffset,
            IntPtr src,
            ulong srcOffset,
            ulong count
        )
        {
            extractor.CopyBlock(
                (byte*)(dest.ToPointer()),
                destOffset,
                (byte*)(src.ToPointer()),
                srcOffset,
                count
            );
        }

        public static unsafe void CopyBlock(
            void* dest,
            uint destOffset,
            void* src,
            uint srcOffset,
            uint count
        )
        {
            extractor.CopyBlock((byte*)dest, destOffset, (byte*)src, srcOffset, count);
        }

        public static unsafe void CopyBlock(
            void* dest,
            ulong destOffset,
            void* src,
            ulong srcOffset,
            ulong count
        )
        {
            extractor.CopyBlock((byte*)dest, destOffset, (byte*)src, srcOffset, count);
        }

        public static unsafe void CopyBlock(void* dest, void* src, uint count)
        {
            extractor.CopyBlock((byte*)dest, 0, (byte*)src, 0, count);
        }

        public static unsafe void CopyBlock(void* dest, void* src, uint destOffset, uint count)
        {
            extractor.CopyBlock((byte*)dest, destOffset, (byte*)src, 0, count);
        }

        public static unsafe void CopyBlock(void* dest, void* src, ulong count)
        {
            extractor.CopyBlock((byte*)dest, 0, (byte*)src, 0, count);
        }

        public static unsafe void CopyBlock(void* dest, void* src, ulong destOffset, ulong count)
        {
            extractor.CopyBlock((byte*)dest, destOffset, (byte*)src, 0, count);
        }

        public static unsafe void Cpblk(byte* dest, byte* src, uint count)
        {
            extractor.CopyBlock(dest, 0, src, 0, count);
        }

        public static unsafe void Cpblk(byte* dest, byte* src, uint destOffset, uint count)
        {
            extractor.CopyBlock(dest, destOffset, src, 0, count);
        }

        public static unsafe void Cpblk(byte* dest, byte* src, ulong count)
        {
            extractor.CopyBlock(dest, 0, src, 0, count);
        }

        public static unsafe void Cpblk(byte* dest, byte* src, ulong destOffset, ulong count)
        {
            extractor.CopyBlock(dest, destOffset, src, 0, count);
        }

        public static unsafe void Cpblk(
            byte* dest,
            uint destOffset,
            byte* src,
            uint srcOffset,
            uint count
        )
        {
            extractor.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static unsafe void Cpblk(
            byte* dest,
            ulong destOffset,
            byte* src,
            ulong srcOffset,
            ulong count
        )
        {
            extractor.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static unsafe void Cpblk(byte[] dest, byte[] src, uint count)
        {
            extractor.CopyBlock(dest, 0, src, 0, count);
        }

        public static unsafe void Cpblk(byte[] dest, byte[] src, uint destOffset, uint count)
        {
            extractor.CopyBlock(dest, destOffset, src, 0, count);
        }

        public static unsafe void Cpblk(byte[] dest, byte[] src, ulong count)
        {
            extractor.CopyBlock(dest, 0, src, 0, count);
        }

        public static unsafe void Cpblk(byte[] dest, byte[] src, ulong destOffset, ulong count)
        {
            extractor.CopyBlock(dest, destOffset, src, 0, count);
        }

        public static unsafe void Cpblk(
            byte[] dest,
            uint destOffset,
            byte[] src,
            uint srcOffset,
            uint count
        )
        {
            extractor.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static unsafe void Cpblk(
            byte[] dest,
            ulong destOffset,
            byte[] src,
            ulong srcOffset,
            ulong count
        )
        {
            extractor.CopyBlock(dest, destOffset, src, srcOffset, count);
        }

        public static unsafe void Cpblk(IntPtr dest, IntPtr src, uint count)
        {
            extractor.CopyBlock((byte*)(dest.ToPointer()), 0, (byte*)(src.ToPointer()), 0, count);
        }

        public static unsafe void Cpblk(IntPtr dest, IntPtr src, uint destOffset, uint count)
        {
            extractor.CopyBlock(
                (byte*)src.ToPointer(),
                destOffset,
                (byte*)dest.ToPointer(),
                0,
                count
            );
        }

        public static unsafe void Cpblk(IntPtr dest, IntPtr src, ulong count)
        {
            extractor.CopyBlock((byte*)(dest.ToPointer()), 0, (byte*)(src.ToPointer()), 0, count);
        }

        public static unsafe void Cpblk(IntPtr dest, IntPtr src, ulong destOffset, ulong count)
        {
            extractor.CopyBlock(
                (byte*)(dest.ToPointer()),
                destOffset,
                (byte*)(src.ToPointer()),
                0,
                count
            );
        }

        public static unsafe void Cpblk(
            IntPtr dest,
            uint destOffset,
            IntPtr src,
            uint srcOffset,
            uint count
        )
        {
            extractor.CopyBlock(
                (byte*)(dest.ToPointer()),
                destOffset,
                (byte*)(src.ToPointer()),
                srcOffset,
                count
            );
        }

        public static unsafe void Cpblk(
            IntPtr dest,
            ulong destOffset,
            IntPtr src,
            ulong srcOffset,
            ulong count
        )
        {
            extractor.CopyBlock(
                (byte*)(dest.ToPointer()),
                destOffset,
                (byte*)(src.ToPointer()),
                srcOffset,
                count
            );
        }

        public static unsafe void Cpblk(
            void* dest,
            uint destOffset,
            void* src,
            uint srcOffset,
            uint count
        )
        {
            extractor.CopyBlock((byte*)dest, destOffset, (byte*)src, srcOffset, count);
        }

        public static unsafe void Cpblk(
            void* dest,
            ulong destOffset,
            void* src,
            ulong srcOffset,
            ulong count
        )
        {
            extractor.CopyBlock((byte*)dest, destOffset, (byte*)src, srcOffset, count);
        }

        public static unsafe void Cpblk(void* dest, void* src, uint count)
        {
            extractor.CopyBlock((byte*)dest, 0, (byte*)src, 0, count);
        }

        public static unsafe void Cpblk(void* dest, void* src, uint destOffset, uint count)
        {
            extractor.CopyBlock((byte*)dest, destOffset, (byte*)src, 0, count);
        }

        public static unsafe void Cpblk(void* dest, void* src, ulong count)
        {
            extractor.CopyBlock((byte*)dest, 0, (byte*)src, 0, count);
        }

        public static unsafe void Cpblk(void* dest, void* src, ulong destOffset, ulong count)
        {
            extractor.CopyBlock((byte*)dest, destOffset, (byte*)src, 0, count);
        }
    }
}
