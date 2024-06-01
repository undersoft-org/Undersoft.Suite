using System.Linq;

namespace Undersoft.SDK.Extracting
{
    public static partial class Extract
    {
        public static unsafe void CopyBlock(byte* dest, byte* src, int count)
        {
            extractor.CopyBlock(dest, 0, src, 0, (uint)count);
        }

        public static unsafe void CopyBlock(byte* dest, byte* src, int destOffset, int count)
        {
            extractor.CopyBlock(dest, (uint)destOffset, src, 0, (uint)count);
        }

        public static unsafe void CopyBlock(byte* dest, byte* src, long count)
        {
            extractor.CopyBlock(dest, 0, src, 0, (ulong)count);
        }

        public static unsafe void CopyBlock(byte* dest, byte* src, long destOffset, long count)
        {
            extractor.CopyBlock(dest, (ulong)destOffset, src, 0, (ulong)count);
        }

        public static unsafe void CopyBlock(
            byte* dest,
            int destOffset,
            byte* src,
            int srcOffset,
            int count
        )
        {
            extractor.CopyBlock(dest, (uint)destOffset, src, (uint)srcOffset, (uint)count);
        }

        public static unsafe void CopyBlock(
            byte* dest,
            long destOffset,
            byte* src,
            long srcOffset,
            long count
        )
        {
            extractor.CopyBlock(dest, (ulong)destOffset, src, (ulong)srcOffset, (ulong)count);
        }

        public static unsafe void CopyBlock(byte[] dest, byte[] src, int count)
        {
            extractor.CopyBlock(dest, 0, src, 0, (uint)count);
        }

        public static unsafe void CopyBlock(byte[] dest, byte[] src, int destOffset, int count)
        {
            extractor.CopyBlock(dest, (uint)destOffset, src, 0, (uint)count);
        }

        public static unsafe void CopyBlock(byte[] dest, byte[] src, long count)
        {
            extractor.CopyBlock(dest, 0, src, 0, (ulong)count);
        }

        public static unsafe void CopyBlock(byte[] dest, byte[] src, long destOffset, long count)
        {
            extractor.CopyBlock(dest, (ulong)destOffset, src, 0, (ulong)count);
        }

        public static unsafe void CopyBlock(
            byte[] dest,
            int destOffset,
            byte[] src,
            int srcOffset,
            int count
        )
        {
            extractor.CopyBlock(dest, (uint)destOffset, src, (uint)srcOffset, (uint)count);
        }

        public static unsafe void CopyBlock(
            byte[] dest,
            long destOffset,
            byte[] src,
            long srcOffset,
            long count
        )
        {
            extractor.CopyBlock(dest, (ulong)destOffset, src, (ulong)srcOffset, (ulong)count);
        }

        public static unsafe void CopyBlock(
            IntPtr dest,
            int destOffset,
            IntPtr src,
            int srcOffset,
            int count
        )
        {
            extractor.CopyBlock(
                (byte*)(dest.ToPointer()),
                (uint)destOffset,
                (byte*)(src.ToPointer()),
                (uint)srcOffset,
                (uint)count
            );
        }

        public static unsafe void CopyBlock(IntPtr dest, IntPtr src, int count)
        {
            extractor.CopyBlock(
                (byte*)(dest.ToPointer()),
                0,
                (byte*)(src.ToPointer()),
                0,
                (uint)count
            );
        }

        public static unsafe void CopyBlock(IntPtr dest, IntPtr src, int destOffset, int count)
        {
            extractor.CopyBlock(
                (byte*)src.ToPointer(),
                (uint)destOffset,
                (byte*)dest.ToPointer(),
                0,
                (uint)count
            );
        }

        public static unsafe void CopyBlock(IntPtr dest, IntPtr src, long count)
        {
            extractor.CopyBlock(
                (byte*)(dest.ToPointer()),
                0,
                (byte*)(src.ToPointer()),
                0,
                (ulong)count
            );
        }

        public static unsafe void CopyBlock(IntPtr dest, IntPtr src, long destOffset, long count)
        {
            extractor.CopyBlock(
                (byte*)(dest.ToPointer()),
                (ulong)destOffset,
                (byte*)(src.ToPointer()),
                0,
                (ulong)count
            );
        }

        public static unsafe void CopyBlock(
            IntPtr dest,
            long destOffset,
            IntPtr src,
            long srcOffset,
            long count
        )
        {
            extractor.CopyBlock(
                (byte*)(dest.ToPointer()),
                (ulong)destOffset,
                (byte*)(src.ToPointer()),
                (ulong)srcOffset,
                (ulong)count
            );
        }

        public static unsafe void CopyBlock(
            void* dest,
            int destOffset,
            void* src,
            int srcOffset,
            int count
        )
        {
            extractor.CopyBlock(
                (byte*)dest,
                (uint)destOffset,
                (byte*)src,
                (uint)srcOffset,
                (uint)count
            );
        }

        public static unsafe void CopyBlock(
            void* dest,
            long destOffset,
            void* src,
            long srcOffset,
            long count
        )
        {
            extractor.CopyBlock(
                (byte*)dest,
                (ulong)destOffset,
                (byte*)src,
                (ulong)srcOffset,
                (ulong)count
            );
        }

        public static unsafe void CopyBlock(void* dest, void* src, int count)
        {
            extractor.CopyBlock((byte*)dest, 0, (byte*)src, 0, (uint)count);
        }

        public static unsafe void CopyBlock(void* dest, void* src, int destOffset, int count)
        {
            extractor.CopyBlock((byte*)dest, (uint)destOffset, (byte*)src, 0, (uint)count);
        }

        public static unsafe void CopyBlock(void* dest, void* src, long count)
        {
            extractor.CopyBlock((byte*)dest, 0, (byte*)src, 0, (ulong)count);
        }

        public static unsafe void CopyBlock(void* dest, void* src, long destOffset, long count)
        {
            extractor.CopyBlock((byte*)dest, (ulong)destOffset, (byte*)src, 0, (ulong)count);
        }
    }
}
