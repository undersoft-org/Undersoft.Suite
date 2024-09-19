// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SVC.Service.Application.Server.Controllers.Open;

using Microsoft.AspNetCore.Authorization;
using Undersoft.SDK.Service;
using Undersoft.SVC.Service.Clients.Abstractions;
using Undersoft.SVC.Service.Contracts.Vaccination;

/// <summary>
/// The contact controller.
/// </summary>
[Authorize]
public class AppointmentController
    : OpenDataRemoteController<long, IVaccinationStore, Appointment, Appointment, ServiceManager>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppointmentController"/>
    /// class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public AppointmentController(IServicer servicer) : base(servicer) { }
}
