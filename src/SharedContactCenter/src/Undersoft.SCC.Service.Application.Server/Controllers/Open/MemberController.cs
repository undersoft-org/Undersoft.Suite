// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Application.Server
// ********************************************************

using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Open;

/// <summary>
/// The contact controller.
/// </summary>
public class MemberController
    : OpenDataRemoteController<
        long,
        IDataStore,
        Contracts.Member,
        Contracts.Member,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MemberController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public MemberController(IServicer servicer) : base(servicer) { }
}
