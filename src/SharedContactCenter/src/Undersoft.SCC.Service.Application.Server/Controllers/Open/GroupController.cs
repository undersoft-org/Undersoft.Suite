using Microsoft.AspNetCore.Authorization;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Open
{
    [AllowAnonymous]
    public class GroupController
        : OpenDataRemoteController<
            long,
            IDataStore,
            Contracts.Group,
            ViewModels.Group,
            ServiceManager
        >
    {
        public GroupController(IServicer servicer) : base(servicer) { }
    }
}
