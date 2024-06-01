using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;
using Undersoft.SDK.Service.Server.Controller.Stream;

namespace Undersoft.SSC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    public class MemberController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Member,
            Contracts.Member, ServiceManager
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
            Contracts.Member, ServiceManager
        >
    {
        public MembersController(IServicer ultimatr) : base(ultimatr) { }
    }

}
