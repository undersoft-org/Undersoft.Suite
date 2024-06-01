using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Stocks
{
    public abstract class SharedStock : StockBase, IIdentifiable
    {
        protected ManualResetEvent WriteWaitEvent { get; private set; }
        protected ManualResetEvent ReadWaitEvent { get; private set; }

        protected SharedStock(
            string path,
            string name,
            long elementSize,
            long length,
            bool ownsSharedMemory,
            bool fixsize
        ) : base(path, name, elementSize, length, ownsSharedMemory, fixsize)
        {
            WriteWaitEvent = new ManualResetEvent(true);
            ReadWaitEvent = new ManualResetEvent(true);
            Id = (SectorId * ClusterId).UniqueKey64();
            TypeId = options.Type.UniqueKey32();
        }

        protected SharedStock(SectorOptions options) : base(options)
        {
            WriteWaitEvent = new ManualResetEvent(true);
            ReadWaitEvent = new ManualResetEvent(true);
            Id = (SectorId * ClusterId).UniqueKey64();
            TypeId = options.Type.UniqueKey32();
        }

        private int _readWriteTimeout = 100;

        public virtual int ReadWriteTimeout
        {
            get { return _readWriteTimeout; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(
                        "ReadWriteTimeout",
                        "Must be larger than -1."
                    );
                _readWriteTimeout = value;
            }
        }

        public bool AcquireReadLock(int millisecondsTimeout = Timeout.Infinite)
        {
            if (!ReadWaitEvent.WaitOne(millisecondsTimeout))
                return false;
            WriteWaitEvent.Reset();
            return true;
        }

        public void ReleaseReadLock()
        {
            WriteWaitEvent.Set();
        }

        public bool AcquireWriteLock(int millisecondsTimeout = Timeout.Infinite)
        {
            if (!WriteWaitEvent.WaitOne(millisecondsTimeout))
                return false;
            ReadWaitEvent.Reset();
            return true;
        }

        public void ReleaseWriteLock()
        {
            ReadWaitEvent.Set();
        }

        #region Writing

        private void WriteWait()
        {
            if (!WriteWaitEvent.WaitOne(ReadWriteTimeout))
                throw new TimeoutException(
                    "The write operation timed out waiting for the write lock WaitEvent. Check your usage of AcquireWriteLock/ReleaseWriteLock and AcquireReadLock/ReleaseReadLock."
                );
        }

        protected override void Write(
            object data,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            WriteWait();
            base.Write(data, position, t, timeout);
        }

        protected override void Write(
            object[] buffer,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            WriteWait();
            base.Write(buffer, position, t, timeout);
        }

        protected override void Write(
            object[] buffer,
            int index,
            int count,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            WriteWait();
            base.Write(buffer, index, count, position, t, timeout);
        }

        protected override void Write(
            nint ptr,
            long length,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            WriteWait();
            base.Write(ptr, length, position);
        }

        protected override void Write(Action<nint> writeFunc, long position = 0)
        {
            WriteWait();
            base.Write(writeFunc, position);
        }

        #endregion

        #region Reading

        private void ReadWait()
        {
            if (!ReadWaitEvent.WaitOne(ReadWriteTimeout))
                throw new TimeoutException(
                    "The read operation timed out waiting for the read lock WaitEvent. Check your usage of AcquireWriteLock/ReleaseWriteLock and AcquireReadLock/ReleaseReadLock."
                );
        }

        protected override object Read(
            object data,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            ReadWait();
            return base.Read(data, position, t, timeout);
        }

        protected override void Read(
            object[] buffer,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            ReadWait();
            base.Read(buffer, position, t, timeout);
        }

        protected override void Read(
            object[] buffer,
            int index,
            int count,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            ReadWait();
            base.Read(buffer, index, count, position, t, timeout);
        }

        protected override void Read(
            nint destination,
            long length,
            long position = 0,
            Type t = null,
            int timeout = 1000
        )
        {
            ReadWait();
            base.Read(destination, length, position);
        }

        protected override void Read(Action<nint> readFunc, long bufferPosition = 0)
        {
            ReadWait();
            base.Read(readFunc, bufferPosition);
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
            {
                (WriteWaitEvent as IDisposable).Dispose();
                (ReadWaitEvent as IDisposable).Dispose();
            }

            base.Dispose(disposeManagedResources);
        }

        #endregion
    }
}
