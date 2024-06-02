﻿using Microsoft.AspNetCore.Authorization;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Server.Controllers.Open
{
    [AllowAnonymous]
    public class EventController : OpenEventController<long, IEventStore, Event, Event>
    {
        public EventController(IServicer servicer) : base(servicer) { }
    }
}