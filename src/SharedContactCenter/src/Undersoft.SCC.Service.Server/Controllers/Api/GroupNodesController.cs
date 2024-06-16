using Microsoft.AspNetCore.Mvc;
using Undersoft.SCC.Service.Contracts;



// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Server.Controllers.Api;

/// <summary>
/// The groups controller.
/// </summary>
[Route($"{StoreRoutes.ApiDataRoute}/GroupNode")]
public class GroupNodesController
    : ApiCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Domain.Entities.Group,
        GroupNode,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupNodesController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public GroupNodesController(IServicer servicer) : base(servicer) { }
}
