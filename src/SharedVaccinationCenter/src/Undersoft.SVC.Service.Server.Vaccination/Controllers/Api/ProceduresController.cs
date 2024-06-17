﻿// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SVC.Service.Server.Vaccination.Controllers.Api;

using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SVC.Service.Contracts;

/// <summary>
/// The contact controller.
/// </summary>
[Route($"{StoreRoutes.ApiDataRoute}/Procedure")]
public class ProceduresController
    : ApiCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Domain.Entities.Procedure,
        Procedure,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PatientNodeController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public ProceduresController(IServicer servicer) : base(servicer) { }
}
