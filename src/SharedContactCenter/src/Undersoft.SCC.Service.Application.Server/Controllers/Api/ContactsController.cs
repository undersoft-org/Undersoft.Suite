using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Rest
{
    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiDataRoute}/Contact")]
    public class ContactsController
        : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.Contact,
            ViewModels.Contact,
            ServiceManager
        >
    {
        public ContactsController(IServicer servicer) : base(servicer) { }
    }
}
