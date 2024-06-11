using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SCC.Service.Server.Controllers.Api;

using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Identifier;
using Undersoft.SDK.Service.Server.Controller.Api;

[Route($"{StoreRoutes.ApiDataRoute}/ContactIdentifier")]
public class ContactIdentifiersController
    : ApiDataRemoteController<
        long,
        IDataStore,
        Identifier<Contracts.Contact>,
        Identifier<Contracts.Contact>,
        ServiceManager
    >
{
    public ContactIdentifiersController(IServicer ultimatr) : base(ultimatr) { }
}
