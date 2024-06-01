using System;
using Microsoft.Win32.SafeHandles;
using Undersoft.SDK.Stocks.MMF.Native;

namespace Undersoft.SDK.Stocks.MMF.Handle
{
#if !NET40Plus

    public sealed class SafeMMFileHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal SafeMMFileHandle()
            : base(true) { }

        internal SafeMMFileHandle(nint handle, bool ownsHandle)
            : base(ownsHandle)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            try
            {
                return UnsafeMethods.CloseHandle(handle);
            }
            finally
            {
                handle = nint.Zero;
            }
        }
    }
#endif
}