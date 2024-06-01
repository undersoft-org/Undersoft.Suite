using System;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Data.Client
{
    public partial class OpenDataClient<TStore> : OpenDataContext where TStore : IDataServiceStore
    {
        public OpenDataClient(Uri serviceUri) : base(serviceUri)
        {
        }
    }
}