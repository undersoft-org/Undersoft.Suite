using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SBC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class ResourceController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Resource,
            Contracts.Resource, ServiceManager
        >
    {
        public ResourceController(IServicer ultimatr) : base(ultimatr) { }
    }


    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Resource")]
    public class ResourcesController
        : ApiCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Resource,
            Contracts.Resource, ServiceManager
        >
    {
        public ResourcesController(IServicer ultimatr) : base(ultimatr) { }
    }

}
