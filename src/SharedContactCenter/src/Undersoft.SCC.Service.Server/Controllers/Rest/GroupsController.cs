using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Server.Controllers.Rest
{
    [Route($"{StoreRoutes.ApiDataRoute}/Group")]
    public class GroupsController
        : ApiCqrsController<long, IEntryStore, IReportStore, Domain.Entities.Group, Contracts.Group, ServiceManager>
    {
        public GroupsController(IServicer servicer) : base(servicer) { }
    }
}
