using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SSC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class ServiceController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.Service,
            Contracts.Service,
            ServiceManager
        >
    {
        public ServiceController(IServicer ultimatr) : base(ultimatr) { }
    }

    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Ecosystem")]
    public class ServicesController
        : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.Service,
            Contracts.Service,
            ServiceManager
        >
    {
        public ServicesController(IServicer ultimatr) : base(ultimatr) { }
    }
}
