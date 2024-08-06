using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GDC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class ResourceController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.Resource,
            Contracts.Resource,
            ServiceManager
        >
    {
        public ResourceController(IServicer ultimatr) : base(ultimatr) { }
    }


    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Resource")]
    public class ResourcesController
          : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.Resource,
            Contracts.Resource,
            ServiceManager
        >
    {
        public ResourcesController(IServicer ultimatr) : base(ultimatr) { }
    }

}
