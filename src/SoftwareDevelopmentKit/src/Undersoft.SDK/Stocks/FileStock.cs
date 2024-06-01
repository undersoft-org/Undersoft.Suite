namespace Undersoft.SDK.Stocks
{
    using System;
    using Undersoft.SDK.Extracting;

    public unsafe class FileStock : SharedStock, ITableStock
    {
        public Type type;

        public object this[int index]
        {
            get
            {
                object item = null;
                Read(item, index, type);
                return item;
            }
            set { Write(value, index, type); }
        }

        public object this[int index, int offset, Type t]
        {
            get
            {
                object item = null;
                Read(item, index, t, 1000);
                return item;
            }
            set { Write(value, index, t, 1000); }
        }

        public void Rewrite(int index, object structure)
        {
            Read(structure, index, type);
        }

        public FileStock(string file, string name, int bufferSize, Type _type)
            : base(file, name, 1, bufferSize, true, true)
        {
            type = _type;
            Open();
        }

        public FileStock(string file, string name, Type _type) : base(file, name, 0, 0, false, true)
        {
            type = _type;
            Open();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1061:DoNotHideBaseClassMethods"
        )]
        public new void Write(object data, long position = 0, Type t = null, int timeout = 1000)
        {
            base.Write(data, position, t, timeout);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1061:DoNotHideBaseClassMethods"
        )]
        public new void Write(object[] data, long position = 0, Type t = null, int timeout = 1000)
        {
            base.Write(data, position, t, timeout);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1061:DoNotHideBaseClassMethods"
        )]
        public new void Write(
            object[] buffer,
            int index,
            int count,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            base.Write(buffer, index, count, position, t, timeout);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1061:DoNotHideBaseClassMethods"
        )]
        public new void Write(
            nint ptr,
            long length,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            AcquireWriteLock();
            base.Write(ptr, length, position);
            ReleaseWriteLock();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1061:DoNotHideBaseClassMethods"
        )]
        public new void Write(Action<nint> writeFunc, long position = 0)
        {
            base.Write(writeFunc, position);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1061:DoNotHideBaseClassMethods"
        )]
        public new void Write(
            byte* ptr,
            long length,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            base.Write(ptr, length, position);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1061:DoNotHideBaseClassMethods"
        )]
        public new object Read(object data, long position = 0, Type t = null, int timeout = 1000)
        {
            return base.Read(data, position, t);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1061:DoNotHideBaseClassMethods"
        )]
        public new void Read(object[] data, long position = 0, Type t = null, int timeout = 1000)
        {
            base.Read(data, position, t, timeout);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1061:DoNotHideBaseClassMethods"
        )]
        public new void Read(
            object[] buffer,
            int index,
            int count,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            AcquireReadLock();
            base.Read(buffer, index, count, position, t, timeout);
            ReleaseReadLock();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1061:DoNotHideBaseClassMethods"
        )]
        public new void Read(
            nint destination,
            long length,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            base.Read(destination, length, position, t, timeout);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1061:DoNotHideBaseClassMethods"
        )]
        public new void Read(
            byte* destination,
            long length,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            base.Read(destination, length, position, t, timeout);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1061:DoNotHideBaseClassMethods"
        )]
        public new void Read(Action<nint> readFunc, long position = 0)
        {
            base.Read(readFunc, position);
        }

        public void CopyTo(ITableStock destination, uint length, int startIndex = 0)
        {
            Extract.CopyBlock(destination.GetStockPtr() + startIndex, GetStockPtr(), length);
        }

        public void Write()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }
    }
}
