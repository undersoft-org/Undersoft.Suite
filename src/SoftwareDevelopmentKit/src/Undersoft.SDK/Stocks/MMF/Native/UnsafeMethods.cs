using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Undersoft.SDK.Stocks.MMF.Handle;

namespace Undersoft.SDK.Stocks.MMF.Native
{
    [SuppressUnmanagedCodeSecurity]
    public class UnsafeMethods
    {
        public UnsafeMethods() { }

#if !NET40Plus

        [DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, ExactSpelling = false)]
        [SecurityCritical]
        internal static extern int FormatMessage(int dwFlags, nint lpSource, int dwMessageId, int dwLanguageId,
            StringBuilder lpBuffer, int nSize, nint va_list_arguments);

        [SecurityCritical]
        internal static string GetMessage(int errorCode)
        {
            StringBuilder stringBuilder = new StringBuilder(512);
            if (FormatMessage(12800, nint.Zero, errorCode, 0, stringBuilder, stringBuilder.Capacity,
                    nint.Zero) != 0)
            {
                return stringBuilder.ToString();
            }

            return string.Concat("UnknownError_Num ", errorCode);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SYSTEM_INFO
        {
            internal _PROCESSOR_INFO_UNION uProcessorInfo;
            public uint dwPageSize;
            public nint lpMinimumApplicationAddress;
            public nint lpMaximumApplicationAddress;
            public nint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort dwProcessorLevel;
            public ushort dwProcessorRevision;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct _PROCESSOR_INFO_UNION
        {
            [FieldOffset(0)] internal uint dwOemId;
            [FieldOffset(0)] internal ushort wProcessorArchitecture;
            [FieldOffset(2)] internal ushort wReserved;
        }

        [Flags]
        public enum FileMapAccess : uint
        {
            FileMapCopy = 0x0001,
            FileMapWrite = 0x0002,
            FileMapRead = 0x0004,
            FileMapAllAccess = 0x001f,
            FileMapExecute = 0x0020,
        }

        [Flags]
        internal enum FileMapProtection : uint
        {
            PageReadonly = 0x02,
            PageReadWrite = 0x04,
            PageWriteCopy = 0x08,
            PageExecuteRead = 0x20,
            PageExecuteReadWrite = 0x40,
            SectionCommit = 0x8000000,
            SectionImage = 0x1000000,
            SectionNoCache = 0x10000000,
            SectionReserve = 0x4000000,
        }

        internal const int ERROR_ALREADY_EXISTS = 0xB7;

        internal const int ERROR_TOO_MANY_OPEN_FILES = 0x4;

        internal const int ERROR_ACCESS_DENIED = 0x5;

        internal const int ERROR_FILE_NOT_FOUND = 0x2;

        [DllImport("kernel32.dll", CharSet = CharSet.None, SetLastError = true)]
        [SecurityCritical]
        internal static extern bool CloseHandle(nint handle);

        [DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
        [SecurityCritical]
        internal static extern SafeMMFileHandle CreateFileMapping(SafeFileHandle hFile, nint lpAttributes,
            FileMapProtection fProtect, int dwMaxSizeHi, int dwMaxSizeLo, string lpName);

        internal static SafeMMFileHandle CreateFileMapping(SafeFileHandle hFile, FileMapProtection flProtect,
            long ddMaxSize, string lpName)
        {
            int hi = (int)(ddMaxSize / int.MaxValue);
            int lo = (int)(ddMaxSize % int.MaxValue);
            return CreateFileMapping(hFile, nint.Zero, flProtect, hi, lo, lpName);
        }

        [DllImport("kernel32.dll")]
        internal static extern void GetSystemInfo([MarshalAs(UnmanagedType.Struct)] ref SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern SafeMMViewHandle MapViewOfFile(
            SafeMMFileHandle hFileMappingObject,
            FileMapAccess dwDesiredAccess,
            uint dwFileOffsetHigh,
            uint dwFileOffsetLow,
            nuint dwNumberOfBytesToMap);

        internal static SafeMMViewHandle MapViewOfFile(SafeMMFileHandle hFileMappingObject,
            FileMapAccess dwDesiredAccess, ulong ddFileOffset, nuint dwNumberofBytesToMap)
        {
            uint hi = (uint)(ddFileOffset / uint.MaxValue);
            uint lo = (uint)(ddFileOffset % uint.MaxValue);
            return MapViewOfFile(hFileMappingObject, dwDesiredAccess, hi, lo, dwNumberofBytesToMap);
        }

        [DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true,
            ThrowOnUnmappableChar = true)]
        internal static extern SafeMMFileHandle OpenFileMapping(
            uint dwDesiredAccess,
            bool bInheritHandle,
            string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool UnmapViewOfFile(nint lpBaseAddress);

#endif
    }
}