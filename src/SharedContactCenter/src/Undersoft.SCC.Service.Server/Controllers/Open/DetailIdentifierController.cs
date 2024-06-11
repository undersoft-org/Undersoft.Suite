// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Server.Controllers.Open;

using Undersoft.SCC.Domain.Entities;

/// <summary>
/// The contact organization controller.
/// </summary>
public class DetailIdentifierController
    : OpenCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Identifier<Detail>,
        Identifier<Contracts.Detail>,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DetailIdentifierController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public DetailIdentifierController(IServicer servicer) : base(servicer) { }
}
