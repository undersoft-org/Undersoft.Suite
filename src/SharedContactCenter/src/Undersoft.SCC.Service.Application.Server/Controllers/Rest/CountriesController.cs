using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Rest
{
    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Country")]
    public class CountriesController
        : ApiCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Country,
            Contracts.Country,
            ServiceManager
        >
    {
        public CountriesController(IServicer servicer) : base(servicer) { }
    }
}
