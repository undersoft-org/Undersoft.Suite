using Microsoft.AspNetCore.Mvc;

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
/// The contacts controller.
/// </summary>
[Route($"{StoreRoutes.ApiDataRoute}/Member")]
public class MembersController
    : ApiCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Domain.Entities.Member,
        Contracts.Member,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MemberNodesController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public MembersController(IServicer servicer) : base(servicer) { }
}
