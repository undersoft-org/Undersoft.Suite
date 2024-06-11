using Microsoft.AspNetCore.Mvc;
using Undersoft.SCC.Service.Contracts;


// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Application.Server
// ********************************************************

using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Api;

/// <summary>
/// The groups controller.
/// </summary>
[Route($"{StoreRoutes.ApiDataRoute}/Group")]
public class GroupsController
    : ApiDataRemoteController<
        long,
        IDataStore,
        Group,
        Group,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupsController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public GroupsController(IServicer servicer) : base(servicer) { }
}
