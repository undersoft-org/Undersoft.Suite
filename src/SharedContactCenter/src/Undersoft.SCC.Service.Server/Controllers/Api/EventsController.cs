using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Server.Controllers.Api
{
    [Route($"{StoreRoutes.ApiEventRoute}/Event")]
    public class EventsController : ApiEventController<long, IEventStore, Event, Event>
    {
        public EventsController(IServicer servicer) : base(servicer) { }
    }
}
