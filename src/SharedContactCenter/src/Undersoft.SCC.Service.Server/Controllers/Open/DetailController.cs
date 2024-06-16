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

using Undersoft.SCC.Service.Contracts;

/// <summary>
/// The contact proffesional controller.
/// </summary>
public class DetailController
    : OpenCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Domain.Entities.Detail,
        Detail,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MemberNodeController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public DetailController(IServicer servicer) : base(servicer) { }
}
