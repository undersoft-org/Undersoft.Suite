using System;
using Microsoft.Win32.SafeHandles;
using Undersoft.SDK.Stocks.MMF.Native;

namespace Undersoft.SDK.Stocks.MMF.Handle
{
#if !NET40Plus

    public sealed class SafeMMViewHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal SafeMMViewHandle()
            : base(true) { }

        internal SafeMMViewHandle(nint handle, bool ownsHandle)
            : base(ownsHandle)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            try
            {
                return UnsafeMethods.UnmapViewOfFile(handle);
            }
            finally
            {
                handle = nint.Zero;
            }
        }

        public unsafe void AcquireIntPtr(ref byte* pointer)
        {
            bool flag = false;
            DangerousAddRef(ref flag);
            pointer = (byte*)handle.ToPointer();
        }

        public void ReleaseIntPtr()
        {
            DangerousRelease();
        }
    }
#endif
}