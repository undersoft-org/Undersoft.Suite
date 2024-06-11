// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Server
// ********************************************************

using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Identifier;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Open;

/// <summary>
/// The contact address controller.
/// </summary>
public class DetailIdentifierController
    : OpenDataRemoteController<
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
    public DetailIdentifierController(IServicer servicer) : base(servicer) { }
}
