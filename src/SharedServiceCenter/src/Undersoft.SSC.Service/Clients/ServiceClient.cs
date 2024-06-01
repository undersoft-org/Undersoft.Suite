using Microsoft.OData.Edm;
using Undersoft.SDK.Service.Data.Client;
using Undersoft.SDK.Service.Data.Remote;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SSC.Domain.Entities;

namespace Undersoft.SSC.Service.Clients;

public class ServiceClient : OpenDataClient<IDataStore>
{
    public ServiceClient(Uri serviceUri) : base(serviceUri) { }

    protected override IEdmModel OnModelCreating(IEdmModel builder)
    {
        this.RemoteSetToSet<Contracts.Service, Application>(r => r.LeftEntity, r => r.Id);

        return base.OnModelCreating(builder);
    }
}
