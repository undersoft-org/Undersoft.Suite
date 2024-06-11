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
/// The contacts controller.
/// </summary>
[Route($"{StoreRoutes.ApiDataRoute}/Contact")]
public class MembersController
    : ApiDataRemoteController<
        long,
        IDataStore,
        Member,
        Member,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MembersController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public MembersController(IServicer servicer) : base(servicer) { }
}
