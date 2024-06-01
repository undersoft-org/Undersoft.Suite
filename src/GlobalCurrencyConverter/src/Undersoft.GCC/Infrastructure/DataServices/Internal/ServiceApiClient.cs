using Undersoft.SDK.Service.Data.Client;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.GCC.Infrastructure.DataServices.Internal;

public class ServiceApiClient : ApiDataClient<IDataStore>
{
    public ServiceApiClient(Uri serviceUri) : base(serviceUri) { }
}
