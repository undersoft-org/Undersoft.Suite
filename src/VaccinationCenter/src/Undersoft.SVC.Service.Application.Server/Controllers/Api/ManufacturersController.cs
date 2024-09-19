// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SVC.Service.Application.Server.Controllers.Api;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SVC.Service.Clients.Abstractions;
using Undersoft.SVC.Service.Contracts.Catalogs;


/// <summary>
/// The contact controller.
/// </summary>
[Authorize]
[Route($"{StoreRoutes.ApiDataRoute}/Manufacturer")]
public class ManufacturersController
     : ApiDataRemoteController<long, ICatalogsStore, Manufacturer,
        Manufacturer,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PatientNodeController"/>
    /// class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public ManufacturersController(IServicer servicer) : base(servicer) { }
}
