using System.IO;
using System.Runtime.InteropServices;
using Undersoft.SDK.Stocks.MMF.Handle;
using Undersoft.SDK.Stocks.MMF.Native;

namespace Undersoft.SDK.Stocks.MMF
{
#if !NET40Plus

    public sealed class MMView : IDisposable
    {
        SafeMMViewHandle _handle;

        public SafeMMViewHandle SafeMemoryMappedViewHandle
        {
            get { return _handle; }
        }

        long _size;
        long _offset;

        public long Size
        {
            get { return _size; }
        }

        public long ViewStartOffset
        {
            get { return _offset; }
        }

        private MMView(SafeMMViewHandle handle, long offset, long size)
        {
            _handle = handle;
            _offset = offset;
            _size = size;
        }

        ~MMView()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposeManagedResources)
        {
            if (_handle != null && !_handle.IsClosed)
                _handle.Dispose();
            _handle = null;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke")]
        internal static MMView CreateView(SafeMMFileHandle safeMemoryMappedFileHandle,
            MMFileAccess access, long offset, long size)
        {
            UnsafeMethods.SYSTEM_INFO info = new UnsafeMethods.SYSTEM_INFO();
            UnsafeMethods.GetSystemInfo(ref info);

            long fileMapStart = offset / info.dwAllocationGranularity * info.dwAllocationGranularity;

            long mapViewSize = offset % info.dwAllocationGranularity + size;

            long viewDelta = offset - fileMapStart;

            SafeMMViewHandle safeHandle = UnsafeMethods.MapViewOfFile(safeMemoryMappedFileHandle,
                access.ToMapViewFileAccess(), (ulong)fileMapStart, new nuint((ulong)mapViewSize));
            var lastWin32Error = Marshal.GetLastWin32Error();
            if (safeHandle.IsInvalid)
            {
                if (lastWin32Error == UnsafeMethods.ERROR_FILE_NOT_FOUND)
                    throw new FileNotFoundException();
                throw new IOException(UnsafeMethods.GetMessage(lastWin32Error));
            }

            return new MMView(safeHandle, viewDelta, size);
        }
    }
#endif
}