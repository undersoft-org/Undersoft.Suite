using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GDC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class ActivityController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.Activity,
            Contracts.Activity,
            ServiceManager
        >
    {
        public ActivityController(IServicer ultimatr) : base(ultimatr) { }
    }


    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Activity")]
    public class ActivitiesController
          : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.Activity,
            Contracts.Activity,
            ServiceManager
        >
    {
        public ActivitiesController(IServicer ultimatr) : base(ultimatr) { }
    }

}
