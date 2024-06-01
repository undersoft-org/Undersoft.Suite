using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SSC.Service.Server.Controllers
{
    [AllowAnonymous]
    public class ServiceResourceController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Resource,
            Contracts.ServiceResource,
            ServiceManager
        >
    {
        public ServiceResourceController(IServicer ultimatr) : base(ultimatr) { }
    }

    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/ServiceResource")]
    public class ServiceResourcesController
        : ApiCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Resource,
            Contracts.ServiceResource,
            ServiceManager
        >
    {
        public ServiceResourcesController(IServicer ultimatr) : base(ultimatr) { }
    }
}
