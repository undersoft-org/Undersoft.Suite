// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Server
// ********************************************************

using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Server.Controllers.Open;

/// <summary>
/// The group controller.
/// </summary>
public class GroupNodeController
    : OpenCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Domain.Entities.Group,
        GroupNode,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupNodeController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public GroupNodeController(IServicer servicer) : base(servicer) { }
}
