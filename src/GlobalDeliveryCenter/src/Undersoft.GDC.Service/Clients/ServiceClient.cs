using Microsoft.OData.Edm;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.GDC.Service.Clients;

public class ServiceClient : OpenDataClient<IDataStore>
{
    public ServiceClient(Uri serviceUri) : base(serviceUri) { }

    protected override IEdmModel OnModelCreating(IEdmModel builder)
    {
        return base.OnModelCreating(builder);
    }
}
