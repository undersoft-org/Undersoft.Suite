// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SVC.Service.Server.Inventory.Controllers.Open;

/// <summary>
/// The event controller.
/// </summary>
public class EventInfoController : OpenEventController<long, IEventStore, Event, EventInfo>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventInfoController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public EventInfoController(IServicer servicer) : base(servicer) { }
}
