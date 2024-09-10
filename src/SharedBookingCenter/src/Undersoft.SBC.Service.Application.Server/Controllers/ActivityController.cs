using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SBC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class ActivityController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Activity,
            Contracts.Activity, ServiceManager
        >
    {
        public ActivityController(IServicer ultimatr) : base(ultimatr) { }
    }


    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Activity")]
    public class ActivitiesController
        : ApiCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Activity,
            Contracts.Activity, ServiceManager
        >
    {
        public ActivitiesController(IServicer ultimatr) : base(ultimatr) { }
    }

}
