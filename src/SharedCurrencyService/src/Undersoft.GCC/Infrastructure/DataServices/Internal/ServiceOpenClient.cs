using Microsoft.OData.Edm;
using Undersoft.SDK.Service.Data.Client;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.GCC.Infrastructure.DataServices.Internal;

public class ServiceOpenClient : OpenDataClient<IDataStore>
{
    public ServiceOpenClient(Uri serviceUri) : base(serviceUri) { }

    protected override IEdmModel OnModelCreating(IEdmModel builder)
    {
        return base.OnModelCreating(builder);
    }
}
