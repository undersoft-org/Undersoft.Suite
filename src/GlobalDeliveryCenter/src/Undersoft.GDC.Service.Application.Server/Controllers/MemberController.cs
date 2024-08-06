using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GDC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class MemberController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.Member,
            Contracts.Member,
            ServiceManager
        >
    {
        public MemberController(IServicer ultimatr) : base(ultimatr) { }
    }


    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Member")]
    public class MembersController
          : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.Member,
            Contracts.Member,
            ServiceManager
        >
    {
        public MembersController(IServicer ultimatr) : base(ultimatr) { }
    }

}
