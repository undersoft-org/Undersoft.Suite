using Microsoft.AspNetCore.Authorization;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Server.Controllers.Open
{
    [AllowAnonymous]
    public class CountryController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.Country,
            Contracts.Country,
            ServiceManager
        >
    {
        public CountryController(IServicer servicer) : base(servicer) { }
    }
}
