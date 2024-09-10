﻿namespace Undersoft.SDK.Ethernet
{
    using System;
    using System.IO;
    using System.Net.Sockets;

    public sealed class EthernetStream : Stream
    {
        internal const int readLimit = 4194304;
        internal const int writeLimit = 65536;

        private Socket socket;
        private int timeout = 0;

        public EthernetStream(Socket socketToStream)
        {
            socket = socketToStream;
            if (socket == null)
                throw new ArgumentNullException("socket");
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { throw new NotSupportedException("dont't ever use in net kind of streams"); }
        }

        public override long Position
        {
            get { throw new NotSupportedException("dont't ever use in net kind of streams"); }
            set { throw new NotSupportedException("dont't ever use in net kind of streams"); }
        }

        public override IAsyncResult BeginRead(
            byte[] buffer,
            int offset,
            int size,
            AsyncCallback asyncCallback,
            object contextObject
        )
        {
            if (size >= readLimit)
            {
                throw new NotSupportedException("reach read Limit 4MB");
            }
            IAsyncResult result = socket.BeginReceive(
                buffer,
                offset,
                size,
                SocketFlags.None,
                asyncCallback,
                contextObject
            );
            return result;
        }

        public override IAsyncResult BeginWrite(
            byte[] buffer,
            int offset,
            int size,
            AsyncCallback asyncCallback,
            object contextObject
        )
        {
            IAsyncResult result = socket.BeginSend(
                buffer,
                offset,
                size,
                SocketFlags.None,
                asyncCallback,
                contextObject
            );
            return result;
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            return socket.EndReceive(asyncResult);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            socket.EndSend(asyncResult);
        }

        public override void Flush()
        {
            throw new NotSupportedException("don't use its a empty method for future use maybe");
        }

        public override int Read(byte[] buffer, int offset, int size)
        {
            if (timeout <= 0)
            {
                if (size >= readLimit)
                {
                    throw new NotSupportedException("reach read Limit 64K");
                }
                return socket.Receive(buffer, offset, Math.Min(size, readLimit), SocketFlags.None);
            }
            else
            {
                if (size >= readLimit)
                {
                    throw new NotSupportedException("reach read Limit 64K");
                }
                IAsyncResult ar = socket.BeginReceive(
                    buffer,
                    offset,
                    size,
                    SocketFlags.None,
                    null,
                    null
                );
                if (timeout > 0 && !ar.IsCompleted)
                {
                    ar.AsyncWaitHandle.WaitOne(timeout, false);
                    if (!ar.IsCompleted)
                        throw new Exception();
                }
                return socket.EndReceive(ar);
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("dont't ever use in socket kind of streams");
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("dont't ever use in net kind of streams");
        }

        public override void Write(byte[] buffer, int offset, int size)
        {
            int tempSize = size;
            while (tempSize > 0)
            {
                size = Math.Min(tempSize, writeLimit);
                socket.Send(buffer, offset, size, SocketFlags.None);
                tempSize -= size;
                offset += size;
            }
        }

        protected override void Dispose(bool disposeIt)
        {
            try
            {
                if (disposeIt)
                    socket.Close();
            }
            finally
            {
                base.Dispose(disposeIt);
            }
        }
    }
}
