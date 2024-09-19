// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Application.Server
// ********************************************************

using Microsoft.AspNetCore.Authorization;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SVC.Service.Application.Server.Controllers.Open;

/// <summary>
/// The event controller.
/// </summary>
[Authorize]
public class EventInfoController : OpenEventController<long, IEventStore, Event, EventInfo>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventInfoController"/>
    /// class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public EventInfoController(IServicer servicer) : base(servicer) { }
}
