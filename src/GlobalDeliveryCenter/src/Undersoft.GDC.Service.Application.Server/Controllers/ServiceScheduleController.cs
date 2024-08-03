using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GDC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class ServiceScheduleController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.ServiceSchedule,
            Contracts.ServiceSchedule,
            ServiceManager
        >
    {
        public ServiceScheduleController(IServicer ultimatr) : base(ultimatr) { }
    }


    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/ServiceSchedule")]
    public class ServiceSchedulesController
          : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.ServiceSchedule,
            Contracts.ServiceSchedule,
            ServiceManager
        >
    {
        public ServiceSchedulesController(IServicer ultimatr) : base(ultimatr) { }
    }

}
