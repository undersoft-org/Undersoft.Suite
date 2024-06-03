using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Rest
{
    [Route($"{StoreRoutes.ApiEventRoute}/Event")]
    public class EventsController : ApiEventController<long, IEventStore, Event, Event>
    {
        public EventsController(IServicer servicer) : base(servicer) { }
    }
}
