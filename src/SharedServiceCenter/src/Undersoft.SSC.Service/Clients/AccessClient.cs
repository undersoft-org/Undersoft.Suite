using Microsoft.OData.Edm;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SSC.Service.Clients;

public class AccessClient : OpenDataClient<IAccountStore>
{
    public AccessClient(Uri serviceUri) : base(serviceUri) { }

    protected override IEdmModel OnModelCreating(IEdmModel builder)
    {
        return base.OnModelCreating(builder);
    }
}
