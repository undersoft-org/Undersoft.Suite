using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GDC.Service.Server.Controllers
{
    [AllowAnonymous]
    public class ServiceMemberController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Member,
            Contracts.ServiceMember,
            ServiceManager
        >
    {
        public ServiceMemberController(IServicer ultimatr) : base(ultimatr) { }
    }

    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/ServiceMember")]
    public class ServiceMembersController
        : ApiCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Member,
            Contracts.ServiceMember,
            ServiceManager
        >
    {
        public ServiceMembersController(IServicer ultimatr) : base(ultimatr) { }
    }
}
