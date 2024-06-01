namespace Undersoft.SDK.Stocks.MMF.Native
{
    using System.Runtime.InteropServices;
    using System.Runtime.Versioning;
    using System;
    using System.Security;
#if !SILVERLIGHT || FEATURE_NETCORE
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Threading;
    using Microsoft.Win32.SafeHandles;
    using Undersoft.SDK.Stocks.MMF.Handle;
#endif

#if !SILVERLIGHT

    [SuppressUnmanagedCodeSecurity]
#endif

    internal static class SafeMethods
    {
#if FEATURE_PAL && !FEATURE_NETCORE
        [DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention =
 CallingConvention.Cdecl)]
        public static extern IntPtr CFStringCreateWithCharacters(
            IntPtr alloc,                
            [MarshalAs(UnmanagedType.LPWStr)] 
            string chars,
            int numChars);
 
        [DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention =
 CallingConvention.Cdecl)]
        public static extern void CFRelease(IntPtr cf);
 
        [DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention =
 CallingConvention.Cdecl)]
        public static extern int CFUserNotificationDisplayAlert(double timeout, uint flags, IntPtr iconUrl, IntPtr soundUrl, IntPtr localizationUrl, IntPtr alertHeader, IntPtr alertMessage, IntPtr defaultButtonTitle, IntPtr alternateButtonTitle, IntPtr otherButtonTitle, ref uint responseFlags);
#endif

        public const int
            MB_RIGHT = 0x00080000,
            MB_RTLREADING = 0x00100000;

#if !FEATURE_PAL && !FEATURE_CORESYSTEM

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern nint GetStockObject(int nIndex);
#endif

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, BestFitMapping = true)]
        [ResourceExposure(ResourceScope.None)]
        public static extern void OutputDebugString(string message);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "MessageBoxW",
            ExactSpelling = true)]
        private static extern int MessageBoxSystem(nint hWnd, string text, string caption, int type);

        [SecurityCritical]
        public static int MessageBox(nint hWnd, string text, string caption, int type)
        {
            try
            {
                return MessageBoxSystem(hWnd, text, caption, type);
            }
            catch (DllNotFoundException)
            {
                return 0;
            }
            catch (EntryPointNotFoundException)
            {
                return 0;
            }
        }

#if !SILVERLIGHT || FEATURE_NETCORE
        public const int
            FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100,
            FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200,
            FORMAT_MESSAGE_FROM_STRING = 0x00000400,
            FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000,
            FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000;

        public const int ERROR_INSUFFICIENT_BUFFER = 0x7A;

#if FEATURE_NETCORE
        [SecurityCritical]
        [System.Security.SuppressUnmanagedCodeSecurity]
#endif
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true,
            BestFitMapping = true)]
        [SuppressMessage("Microsoft.Security", "CA2101:SpecifyMarshalingForPInvokeStringArguments")]
        [ResourceExposure(ResourceScope.None)]
        public static extern int FormatMessage(int dwFlags, nint lpSource_mustBeNull, uint dwMessageId,
            int dwLanguageId, StringBuilder lpBuffer, int nSize, nint[] arguments);

#if FEATURE_NETCORE
        [SecurityCritical]
        [System.Security.SuppressUnmanagedCodeSecurity]
#endif

#if FEATURE_NETCORE
        [SecurityCritical]
        [System.Security.SuppressUnmanagedCodeSecurity]
#else
        [ResourceExposure(ResourceScope.None)]
#endif
        [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern bool CloseHandle(nint handle);
#endif

#if !SILVERLIGHT || FEATURE_NETCORE

#if FEATURE_NETCORE
        [SecurityCritical]
        [System.Security.SuppressUnmanagedCodeSecurity]
#endif
        [DllImport("kernel32.dll")]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool QueryPerformanceCounter(out long value);

#if FEATURE_NETCORE
        [SecurityCritical]
        [System.Security.SuppressUnmanagedCodeSecurity]
#endif
        [DllImport("kernel32.dll")]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool QueryPerformanceFrequency(out long value);
#endif

#if !SILVERLIGHT
#if !FEATURE_PAL
        public const int
            FORMAT_MESSAGE_MAX_WIDTH_MASK = 0x000000FF,
            FORMAT_MESSAGE_FROM_HMODULE = 0x00000800;

        [DllImport("user32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        [ResourceExposure(ResourceScope.Machine)]
        public static extern int RegisterWindowMessage(string msg);

#if DEBUG

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern int GetCurrentThreadId();

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern int GetWindowThreadProcessId(HandleRef hWnd, out int lpdwProcessId);
#endif

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern nint LoadLibrary(string libFilename);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern bool FreeLibrary(HandleRef hModule);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool GetComputerName(StringBuilder lpBuffer, int[] nSize);

        public static unsafe int InterlockedCompareExchange(nint pDestination, int exchange, int compare)
        {
            return Interlocked.CompareExchange(ref *(int*)pDestination.ToPointer(), exchange, compare);
        }

#endif

        [DllImport("kernel32.dll", SetLastError = true)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool IsWow64Process(SafeProcessingHandle hProcess, ref bool Wow64Process);

        [StructLayout(LayoutKind.Sequential)]
        internal class PROCESS_INFORMATION
        {
            public nint hProcess = nint.Zero;

            public nint hThread = nint.Zero;

            public int dwProcessId = 0;

            public int dwThreadId = 0;
        }

#endif

#if !SILVERLIGHT || FEATURE_NETCORE
#if FEATURE_NETCORE
        [SecurityCritical]
#endif
#if FEATURE_NETCORE
        [SecurityCritical]
#endif
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
        [ResourceExposure(ResourceScope.Machine)]
        internal static extern SafeWaitHandle OpenSemaphore(int desiredAccess, bool inheritHandle, string name);

#if FEATURE_NETCORE
        [SecurityCritical]
#else
        [ResourceExposure(ResourceScope.Machine)]
#endif
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool ReleaseSemaphore(SafeWaitHandle handle, int releaseCount, out int previousCount);

#endif
    }
}