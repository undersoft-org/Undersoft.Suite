using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Server.Controllers.Api
{
    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Contact")]
    public class ContactsController
        : ApiCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Contact,
            Contracts.Contact,
            ServiceManager
        >
    {
        public ContactsController(IServicer servicer) : base(servicer) { }
    }
}
