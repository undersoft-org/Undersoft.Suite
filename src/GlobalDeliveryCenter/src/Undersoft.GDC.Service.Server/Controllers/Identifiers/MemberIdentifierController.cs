using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;
using Undersoft.GDC.Domain.Entities;

namespace Undersoft.GDC.Service.Server.Controllers
{
    [AllowAnonymous]
    public class MemberIdentifierController
       : OpenCqrsController<
           long,
           IEntryStore,
           IReportStore,
           Identifier<Member>,
           Identifier<Contracts.Member>,
           ServiceManager
       >
    {
        public MemberIdentifierController(IServicer ultimatr) : base(ultimatr) { }
    }


    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/MemberIdentifier")]
    public class MemberIdentifiersController
        : ApiCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Identifier<Member>,
            Identifier<Contracts.Member>,
            ServiceManager
        >
    {
        public MemberIdentifiersController(IServicer ultimatr) : base(ultimatr) { }
    }
}

