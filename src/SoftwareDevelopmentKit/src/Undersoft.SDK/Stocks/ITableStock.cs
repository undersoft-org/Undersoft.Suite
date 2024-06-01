using System;
using Undersoft.SDK.Extracting;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Diagnostics;
using System.Threading;
using Undersoft.SDK.Uniques;
using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Stocks
{
    public interface ITableStock<T> : ITableStock { }

    public interface ITableStock : IStock
    {
        void Rewrite(int index, object structure);

        bool Exists { get; set; }
        bool FixSize { get; set; }

        string Path { get; set; }

        ushort ClusterId { get; set; }
        ushort SectorId { get; set; }

        long BufferSize { get; set; }
        long UsedSize { get; set; }
        long FreeSize { get; set; }

        int ItemSize { get; set; }
        int ItemCount { get; set; }
        long ItemCapacity { get; set; }

        long SharedMemorySize { get; }
        long UsedMemorySize { get; }
        long FreeMemorySize { get; }

        void ReadHeader();

        void WriteHeader();

        nint GetStockPtr();

        void CopyTo(ITableStock destination, uint length, int startIndex = 0);

        void Write(object data, long position = 0, Type t = null, int timeout = 1000);
        void Write(object[] buffer, long position = 0, Type t = null, int timeout = 1000);
        void Write(
            object[] buffer,
            int index,
            int count,
            long position = 0,
            Type t = null,
            int timeout = 1000
        );
        void Write(
            nint source,
            long length,
            long position = 0,
            Type t = null,
            int timeout = 1000
        );
        unsafe void Write(
            byte* source,
            long length,
            long position = 0,
            Type t = null,
            int timeout = 1000
        );

        object Read(object data, long position = 0, Type t = null, int timeout = 1000);
        void Read(object[] buffer, long position = 0, Type t = null, int timeout = 1000);
        void Read(
            object[] buffer,
            int index,
            int count,
            long position = 0,
            Type t = null,
            int timeout = 1000
        );
        void Read(
            nint destination,
            long length,
            long position = 0,
            Type t = null,
            int timeout = 1000
        );
        unsafe void Read(
            byte* destination,
            long length,
            long position = 0,
            Type t = null,
            int timeout = 1000
        );

        void Dispose();
    }
}
