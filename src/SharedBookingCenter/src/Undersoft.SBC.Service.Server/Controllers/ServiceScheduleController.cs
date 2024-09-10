using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SBC.Service.Server.Controllers
{
    [AllowAnonymous]
    public class ServiceScheduleController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Schedule,
            Contracts.ServiceSchedule,
            ServiceManager
        >
    {
        public ServiceScheduleController(IServicer ultimatr) : base(ultimatr) { }
    }

    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/ServiceSchedule")]
    public class ServiceSchedulesController
        : ApiCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Schedule,
            Contracts.ServiceSchedule,
            ServiceManager
        >
    {
        public ServiceSchedulesController(IServicer ultimatr) : base(ultimatr) { }
    }
}
