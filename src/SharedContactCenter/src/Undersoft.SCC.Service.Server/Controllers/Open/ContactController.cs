using Microsoft.AspNetCore.Authorization;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Server.Controllers.Open
{
    [AllowAnonymous]
    public class ContactController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Contact,
            Contact,
            ServiceManager
        >
    {
        public ContactController(IServicer servicer) : base(servicer) { }
    }
}
