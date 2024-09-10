using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SBC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class ServiceActivityController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.ServiceActivity,
            Contracts.ServiceActivity,
            ServiceManager
        >
    {
        public ServiceActivityController(IServicer ultimatr) : base(ultimatr) { }
    }


    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/ServiceActivity")]
    public class ServiceActivitiesController
          : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.ServiceActivity,
            Contracts.ServiceActivity,
            ServiceManager
        >
    {
        public ServiceActivitiesController(IServicer ultimatr) : base(ultimatr) { }
    }

}
