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
/// The countries controller.
/// </summary>
[Route($"{StoreRoutes.ApiDataRoute}/Country")]
public class CountriesController
    : ApiDataRemoteController<
        long,
        IDataStore,
        Contracts.Country,
        Contracts.Country,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CountriesController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public CountriesController(IServicer servicer) : base(servicer) { }
}
