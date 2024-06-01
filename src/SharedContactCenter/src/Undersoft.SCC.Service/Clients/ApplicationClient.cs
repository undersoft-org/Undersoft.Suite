using Microsoft.OData.Edm;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SCC.Service.Clients;

public class ApplicationClient : OpenDataClient<IDataStore>
{
    public ApplicationClient(Uri serviceUri) : base(serviceUri) { }

    protected override IEdmModel OnModelCreating(IEdmModel builder)
    {

        return base.OnModelCreating(builder);
    }
}
