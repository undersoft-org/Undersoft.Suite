using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SSC.Service.Server.Controllers
{
    [AllowAnonymous]
    public class ApplicationController
        : OpenDataRemoteController<
            long,
            IDataStore,            
            Contracts.Application,
            Contracts.Application, ServiceManager
        >
    {
        public ApplicationController(IServicer ultimatr) : base(ultimatr) { }
    }

    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Application")]
    public class ApplicationsController
        : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.Application,
            Contracts.Application, ServiceManager
        >
    {
        public ApplicationsController(IServicer ultimatr) : base(ultimatr) { }
    }
}
