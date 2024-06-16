// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Server
// ********************************************************

using Microsoft.AspNetCore.Mvc;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Identifier;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Api;

/// <summary>
/// The contact address controller.
/// </summary>
[Route($"{StoreRoutes.ApiDataRoute}/DetailIdentifier")]
public class DetailIdentifiersController
    : ApiDataRemoteController<
        long,
        IDataStore,
       Identifier<Detail>,
       Identifier<Detail>,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DetailIdentifiersController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public DetailIdentifiersController(IServicer servicer) : base(servicer) { }
}
