// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SVC.Service.Application.Server.Controllers.Api;

using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SVC.Service.Clients.Abstractions;
using Undersoft.SVC.Service.Contracts.Vaccination;

/// <summary>
/// The contact controller.
/// </summary>
[Route($"{StoreRoutes.ApiDataRoute}/PostSymptom")]
public class PostSymptomsController
    : ApiDataRemoteController<long, IVaccinationStore, PostSymptom, PostSymptom, ServiceManager>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PatientNodeController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public PostSymptomsController(IServicer servicer) : base(servicer) { }
}
