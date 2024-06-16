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

using Microsoft.AspNetCore.Mvc;
using Undersoft.SCC.Domain.Entities;

/// <summary>
/// The contact organization controller.
/// </summary>
[Route($"{StoreRoutes.ApiDataRoute}/SettingIdentifier")]
public class SettingIdentifiersController
    : ApiCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Identifier<Setting>,
        Identifier<Contracts.Setting>,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingIdentifiersController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public SettingIdentifiersController(IServicer servicer) : base(servicer) { }
}
