using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Security;
using Undersoft.SDK.Stocks.MMF.Handle;
using Undersoft.SDK.Stocks.MMF.Native;

namespace Undersoft.SDK.Stocks.MMF
{
    public sealed class MMFile : IDisposable
    {
        SafeMMFileHandle _handle;

        public SafeMMFileHandle SafeMemoryMappedFileHandle
        {
            [SecurityCritical]
            get { return _handle; }
        }

        private MMFile(SafeMMFileHandle handle)
        {
            _handle = handle;
        }

        ~MMFile()
        {
            Dispose(false);
        }

        public static MMFile CreateNew(string path, string mapName, long capacity, out bool exists,
            bool readCapacity = false)
        {
            if (string.IsNullOrEmpty(mapName) || string.IsNullOrEmpty(path))
                throw new ArgumentException("mapName cannot be null or empty.");

            if (capacity <= 0)
                throw new ArgumentOutOfRangeException("capacity", "Value must be larger than 0.");

            if (nint.Size == 4 && capacity > 1024 * 1024 * 1024 * (long)4)
                throw new ArgumentOutOfRangeException("capacity",
                    "The capacity cannot be greater than the size of the system's logical address space.");

            return new MMFile(DoCreate(path, mapName, capacity, out exists));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke"), SecurityCritical]
        private static SafeMMFileHandle DoCreate(string path, string mapName, long capacity, out bool exists,
            bool readCapacity = false)
        {
            exists = File.Exists(path);
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            if (exists && file.Length != capacity)
            {
                if (!readCapacity)
                {
                    capacity = file.Length;
                    file.Close();
                    File.Copy(path, path + "_old", true);
                    File.Delete(path);
                    file = File.Open(path, FileMode.OpenOrCreate);
                }
                else
                {
                    byte[] byt = new byte[8];
                    file.Read(byt, 0, 8);
                    capacity = BitConverter.ToInt32(byt, 0);
                }
            }

            SafeMMFileHandle safeHandle = null;
            try
            {
                SafeFileHandle fileHandle = file.SafeFileHandle;
                safeHandle = UnsafeMethods.CreateFileMapping(fileHandle,
                    (UnsafeMethods.FileMapProtection)MMFileAccess.ReadWrite,
                capacity, mapName);

                if (safeHandle == null || safeHandle.IsInvalid)
                    throw new InvalidOperationException("Cannot create file mapping");
            }
            catch
            {
                CleanupFile(file, exists, path);
                throw;
            }

            return safeHandle;
        }

        [SecurityCritical]
        public MMViewAccessor CreateViewAccessor(long offset, long size,
            MMFileAccess access = MMFileAccess.ReadWrite)
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", "Value must be non-negative");
            if (size < 0)
                throw new ArgumentOutOfRangeException("size", "Value must be positive or zero for default size");
            if (nint.Size == 4 && size > 1024 * 1024 * 1024 * (long)4)
                throw new ArgumentOutOfRangeException("size",
                    "The capacity cannot be greater than the size of the system's logical address space.");
            MMView memoryMappedView = MMView.CreateView(_handle, access, offset, size);
            return new MMViewAccessor(memoryMappedView);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposeManagedResources)
        {
            if (_handle != null && !_handle.IsClosed)
            {
                _handle.Dispose();
                _handle = null;
            }
        }

        public static MMFile OpenExisting(string mapName)
        {
            SafeMMFileHandle safeMemoryMappedFileHandle = null;
            try
            {
                safeMemoryMappedFileHandle =
                UnsafeMethods.OpenFileMapping((uint)MMFileRights.ReadWrite, false, mapName);

                if (safeMemoryMappedFileHandle == null || safeMemoryMappedFileHandle.IsInvalid)
                    throw new InvalidOperationException("Cannot open file mapping");
            }
            catch
            {
                throw new InvalidOperationException("Cannot create file mapping");
            }

            return new MMFile(safeMemoryMappedFileHandle);
        }

        private static void CleanupFile(FileStream fileStream, bool existed, string path)
        {
            fileStream.Dispose();
            if (!existed)
            {
                File.Delete(path);
            }
        }
    }
}