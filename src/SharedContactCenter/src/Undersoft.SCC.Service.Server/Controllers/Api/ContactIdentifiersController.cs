using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Server.Controllers.Api;

using Undersoft.SCC.Domain.Entities;

[Route($"{StoreRoutes.ApiDataRoute}/ContactIdentifier")]
public class ContactIdentifiersController
    : ApiCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Identifier<Contact>,
        Identifier<Contracts.Contact>,
        ServiceManager
    >
{
    public ContactIdentifiersController(IServicer ultimatr) : base(ultimatr) { }
}

