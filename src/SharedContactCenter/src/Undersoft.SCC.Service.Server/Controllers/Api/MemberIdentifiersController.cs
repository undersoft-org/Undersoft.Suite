using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Server.Controllers.Api;

using Undersoft.SCC.Domain.Entities;

[Route($"{StoreRoutes.ApiDataRoute}/ContactIdentifier")]
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

