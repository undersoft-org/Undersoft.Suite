using Microsoft.AspNetCore.Authorization;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Open
{
    [AllowAnonymous]
    public class ContactPersonalController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.Contacts.ContactPersonal,
            Contracts.Contacts.ContactPersonal,
            ServiceManager
        >
    {
        public ContactPersonalController(IServicer servicer) : base(servicer) { }
    }
}
