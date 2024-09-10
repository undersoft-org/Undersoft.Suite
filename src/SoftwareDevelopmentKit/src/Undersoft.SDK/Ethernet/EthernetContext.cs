using System.Net;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Ethernet
{
    public class EthernetContext
    {
        public string MethodTypeName;
        public string MethodName;

        public IPEndPoint LocalEndPoint;

        public IPEndPoint RemoteEndPoint;

        private Type contentType;

        public TransitComplexity Complexity { get; set; } = TransitComplexity.Standard;

        public Type ContentType
        {
            get
            {
                if (contentType == null && ContentTypeName != null)
                    ContentType = AssemblyUtilities.FindType(ContentTypeName);
                return contentType;
            }
            set
            {
                if (value != null)
                {
                    ContentTypeName = value.FullName;
                    contentType = value;
                }
            }
        }

        public string ContentTypeName { get; set; }

        public string Echo { get; set; }

        public int Errors { get; set; }

        public EthernetSite IdentitySite { get; set; } = EthernetSite.Client;

        public int ItemsCount { get; set; } = 0;

        public bool ReceiveMessage { get; set; } = true;

        public bool SendMessage { get; set; } = true;

        public bool Synchronic { get; set; } = false;
    }
}
