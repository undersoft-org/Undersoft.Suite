using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Identifier;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GDC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class MemberIdentifierController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Identifier<Contracts.Member>,
            Identifier<Contracts.Member>,
            ServiceManager
        >
    {
        public MemberIdentifierController(IServicer ultimatr) : base(ultimatr) { }
    }

    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/MemberIdentifier")]
    public class MemberIdentifiersController
        : ApiDataRemoteController<
            long,
            IDataStore,
            Identifier<Contracts.Member>,
            Identifier<Contracts.Member>,
            ServiceManager
        >
    {
        public MemberIdentifiersController(IServicer ultimatr) : base(ultimatr) { }
    }
}
