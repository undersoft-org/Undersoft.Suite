﻿// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SVC.Service.Application.Server.Controllers.Open;

using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Server.Controller.Open;
using Undersoft.SVC.Service.Contracts;



/// <summary>
/// The contact controller.
/// </summary>
public class OfficeController
    : OpenCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Domain.Entities.Office,
        Office,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PatientNodeController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public OfficeController(IServicer servicer) : base(servicer) { }
}