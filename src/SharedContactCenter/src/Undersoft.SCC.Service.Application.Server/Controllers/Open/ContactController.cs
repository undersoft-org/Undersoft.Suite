using Microsoft.AspNetCore.Authorization;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Open
{
    [AllowAnonymous]
    public class ContactController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.Contact,
            ViewModels.Contact,
            ServiceManager
        >
    {
        public ContactController(IServicer servicer) : base(servicer) { }
    }
}
