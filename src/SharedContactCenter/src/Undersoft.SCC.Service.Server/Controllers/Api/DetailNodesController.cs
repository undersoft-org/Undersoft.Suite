// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Server
// ********************************************************

using Microsoft.AspNetCore.Mvc;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Server.Controllers.Api;
/// <summary>
/// The contact proffesional controller.
/// </summary>
[Route($"{StoreRoutes.ApiDataRoute}/DetailNode")]
public class DetailNodesController
    : ApiCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Domain.Entities.Detail,
        DetailNode,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContactController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public DetailNodesController(IServicer servicer) : base(servicer) { }
}
