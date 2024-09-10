using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace Undersoft.SDK.Ethernet
{
    [Serializable]
    public class TransitHeader : ITransitable, IDisposable
    {
        [NonSerialized]
        private EthernetTransit transaction;

        public TransitHeader()
        {
            Context = new EthernetContext();
            OutputChunks = 0;
            InputChunks = 0;
        }

        public TransitHeader(EthernetTransit _transaction)
        {
            Context = new EthernetContext();
            transaction = _transaction;
            OutputChunks = 0;
            InputChunks = 0;
        }

        public TransitHeader(EthernetTransit _transaction, ITransitContext context)
        {
            Context = new EthernetContext();
            Context.LocalEndPoint = (IPEndPoint)context.Listener.LocalEndPoint;
            Context.RemoteEndPoint = (IPEndPoint)context.Listener.RemoteEndPoint;
            transaction = _transaction;
            OutputChunks = 0;
            InputChunks = 0;
        }

        public TransitHeader(
            EthernetTransit _transaction,
            ITransitContext context,
            EthernetSite site
        )
        {
            Context = new EthernetContext();
            Context.LocalEndPoint = (IPEndPoint)context.Listener.LocalEndPoint;
            Context.RemoteEndPoint = (IPEndPoint)context.Listener.RemoteEndPoint;
            Context.IdentitySite = site;
            transaction = _transaction;
            OutputChunks = 0;
            InputChunks = 0;
        }

        public TransitHeader(EthernetTransit _transaction, EthernetSite site)
        {
            Context = new EthernetContext();
            Context.IdentitySite = site;
            transaction = _transaction;
            OutputChunks = 0;
            InputChunks = 0;
        }

        public object Data { get; set; }

        public EthernetContext Context { get; set; }

        public int InputChunks { get; set; }

        public int ItemsCount
        {
            get { return Context.ItemsCount; }
        }

        public int CurrentChunk { get; set; }

        public int OutputChunks { get; set; }

        public void BindContext(ITransitContext context)
        {
            Context.LocalEndPoint = (IPEndPoint)context.Listener.LocalEndPoint;
            Context.RemoteEndPoint = (IPEndPoint)context.Listener.RemoteEndPoint;
        }

        public object Deserialize(ITransitBuffer buffer)
        {
            return null;
        }

        public object Deserialize(Stream fromstream)
        {
            return null;
        }

        public void Dispose()
        {
            Data = null;
        }

        public object GetHeader()
        {
            return this;
        }

        public object[] GetMessage()
        {
            return null;
        }

        public int Serialize(ITransitBuffer buffer, int offset, int batchSize)
        {
            buffer.Output = this.ToJsonBytes();
            return (int)buffer.Output.Length;
        }

        public int Serialize(Stream tostream, int offset, int batchSize)
        {
            this.ToJsonStream(tostream);
            return (int)tostream.Length;
        }
    }
}
