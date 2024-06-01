using Microsoft.OData.Edm;
using System;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Data.Client
{
    public partial class StreamDataClient<TStore> where TStore : IDataServiceStore
    {
        public StreamDataClient(Uri serviceUri)
        {
        }
    }
}