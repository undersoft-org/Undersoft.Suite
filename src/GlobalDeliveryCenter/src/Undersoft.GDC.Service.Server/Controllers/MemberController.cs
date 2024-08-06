using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GDC.Service.Server.Controllers
{
    [AllowAnonymous]
    public class MemberController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Member,
            Contracts.Member,
            ServiceManager
        >
    {
        public MemberController(IServicer ultimatr) : base(ultimatr) { }
    }

    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Member")]
    public class MembersController
        : ApiCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Member,
            Contracts.Member,
            ServiceManager
        >
    {
        public MembersController(IServicer ultimatr) : base(ultimatr) { }
    }
}
