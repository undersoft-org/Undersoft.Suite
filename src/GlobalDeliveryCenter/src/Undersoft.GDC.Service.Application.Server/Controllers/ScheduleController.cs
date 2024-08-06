using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GDC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class ScheduleController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.Schedule,
            Contracts.Schedule,
            ServiceManager
        >
    {
        public ScheduleController(IServicer ultimatr) : base(ultimatr) { }
    }


    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Schedule")]
    public class SchedulesController
          : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.Schedule,
            Contracts.Schedule,
            ServiceManager
        >
    {
        public SchedulesController(IServicer ultimatr) : base(ultimatr) { }
    }

}
