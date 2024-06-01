using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Server.Controllers.Rest
{
    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Country")]
    public class CountriesController
        : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.Country,
            Contracts.Country,
            ServiceManager
        >
    {
        public CountriesController(IServicer servicer) : base(servicer) { }
    }
}
