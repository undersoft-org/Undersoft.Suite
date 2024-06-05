using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Rest
{
    [Route($"{StoreRoutes.ApiDataRoute}/Group")]
    public class GroupsController
        : ApiDataRemoteController<
            long,
            IDataStore,
            Contracts.Group,
            ViewModels.Group,
            ServiceManager
        >
    {
        public GroupsController(IServicer servicer) : base(servicer) { }
    }
}
