using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SCC.Service.Server.Controllers.Api;

using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Identifier;
using Undersoft.SDK.Service.Server.Controller.Api;

[Route($"{StoreRoutes.ApiDataRoute}/ContactIdentifier")]
public class MemberIdentifiersController
    : ApiDataRemoteController<
        long,
        IDataStore,
        Identifier<Member>,
        Identifier<Member>,
        ServiceManager
    >
{
    public MemberIdentifiersController(IServicer ultimatr) : base(ultimatr) { }
}
