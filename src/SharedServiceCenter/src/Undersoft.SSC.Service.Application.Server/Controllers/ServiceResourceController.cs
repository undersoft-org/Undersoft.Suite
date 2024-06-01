using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SSC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class ServiceResourceController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.ServiceResource,
            Contracts.ServiceResource,
            ServiceManager
        >
    {
        public ServiceResourceController(IServicer ultimatr) : base(ultimatr) { }
    }


    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/ServiceResources")]
    public class ServiceResourcesController
          : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.ServiceResource,
            Contracts.ServiceResource,
            ServiceManager
        >
    {
        public ServiceResourcesController(IServicer ultimatr) : base(ultimatr) { }
    }

}
