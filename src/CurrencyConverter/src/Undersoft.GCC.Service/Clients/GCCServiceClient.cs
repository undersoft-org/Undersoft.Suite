using Microsoft.OData.Edm;
using Undersoft.SDK.Service.Data.Client;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.GCC.Service.Clients;

public class GCCServiceClient : OpenDataClient<IDataStore>
{
    public GCCServiceClient(Uri serviceUri) : base(serviceUri) { }

    protected override IEdmModel OnModelCreating(IEdmModel builder)
    {
        return base.OnModelCreating(builder);
    }
}
