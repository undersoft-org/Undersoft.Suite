using Microsoft.OData.Edm;
using Undersoft.SDK.Service.Data.Client;
using Undersoft.SDK.Service.Data.Remote;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SSC.Domain.Entities;

namespace Undersoft.SSC.Service.Clients;

public class ApplicationClient : OpenDataClient<IDataStore>
{
    public ApplicationClient(Uri serviceUri) : base(serviceUri) { }

    protected override IEdmModel OnModelCreating(IEdmModel builder)
    {
        this.RemoteSetToSet<Domain.Entities.Service, Contracts.Application>(
            r => r.RightEntity,
            r => r.Id
        );

        return base.OnModelCreating(builder);
    }
}
