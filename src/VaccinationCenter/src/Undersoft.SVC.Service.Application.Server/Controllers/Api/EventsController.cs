using Microsoft.AspNetCore.Mvc;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Application.Server
// ********************************************************

using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SVC.Service.Application.Server.Controllers.Api;

/// <summary>
/// The events controller.
/// </summary>
[Route($"{StoreRoutes.ApiEventRoute}/Event")]
public class EventsController : ApiEventController<long, IEventStore, Event, Event>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventsController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public EventsController(IServicer servicer) : base(servicer) { }
}
