using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;
using Undersoft.SDK.Stocks.MMF.Native;

namespace Undersoft.SDK.Stocks.MMF.Handle
{
    [SuppressUnmanagedCodeSecurity]
    internal sealed class SafeProcessingHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal static SafeProcessingHandle InvalidHandle = new SafeProcessingHandle(nint.Zero);

        internal SafeProcessingHandle() : base(true) { }

        internal SafeProcessingHandle(nint handle) : base(true)
        {
            SetHandle(handle);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern SafeProcessingHandle OpenProcess(int access, bool inherit, int processId);

        internal void InitialSetHandle(nint h)
        {
            Debug.Assert(IsInvalid, "Safe handle should only be Set once");
            handle = h;
        }

        protected override bool ReleaseHandle()
        {
            return SafeMethods.CloseHandle(handle);
        }
    }
}