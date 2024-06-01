using Undersoft.SDK.Stocks.MMF.Native;

namespace Undersoft.SDK.Stocks.MMF
{
#if !NET40Plus

    public enum MMFileAccess : uint
    {
        Read = 2,

        ReadWrite = 4,

        CopyOnWrite = 8,

        ReadExecute = 32,

        ReadWriteExecute = 64
    }

    internal static class MemoryMappedFileAccessExtensions
    {
        internal static UnsafeMethods.FileMapAccess ToMapViewFileAccess(this MMFileAccess access)
        {
            switch (access)
            {
                case MMFileAccess.Read:
                    return UnsafeMethods.FileMapAccess.FileMapRead;
                case MMFileAccess.ReadWrite:
                    return UnsafeMethods.FileMapAccess.FileMapRead | UnsafeMethods.FileMapAccess.FileMapWrite;
                case MMFileAccess.ReadExecute:
                    return UnsafeMethods.FileMapAccess.FileMapRead | UnsafeMethods.FileMapAccess.FileMapExecute;
                case MMFileAccess.ReadWriteExecute:
                    return UnsafeMethods.FileMapAccess.FileMapRead | UnsafeMethods.FileMapAccess.FileMapWrite |
                           UnsafeMethods.FileMapAccess.FileMapExecute;
                default:
                    return UnsafeMethods.FileMapAccess.FileMapAllAccess;
            }
        }
    }
#endif
}