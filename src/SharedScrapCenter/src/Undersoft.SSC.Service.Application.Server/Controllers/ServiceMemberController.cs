using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SSC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class ServiceMemberController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.ServiceMember,
            Contracts.ServiceMember,
            ServiceManager
        >
    {
        public ServiceMemberController(IServicer ultimatr) : base(ultimatr) { }
    }


    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/ServiceMember")]
    public class ServiceMembersController
          : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.ServiceMember,
            Contracts.ServiceMember,
            ServiceManager
        >
    {
        public ServiceMembersController(IServicer ultimatr) : base(ultimatr) { }
    }

}
