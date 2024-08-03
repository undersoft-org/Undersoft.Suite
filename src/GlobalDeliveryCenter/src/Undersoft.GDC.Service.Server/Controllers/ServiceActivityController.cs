using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GDC.Service.Server.Controllers
{
    [AllowAnonymous]
    public class ServiceActivityController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Activity,
            Contracts.ServiceActivity,
            ServiceManager
        >
    {
        public ServiceActivityController(IServicer ultimatr) : base(ultimatr) { }
    }

    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/ServiceActivity")]
    public class ServiceActivitiesController
        : ApiCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Activity,
            Contracts.ServiceActivity,
            ServiceManager
        >
    {
        public ServiceActivitiesController(IServicer ultimatr) : base(ultimatr) { }
    }
}
