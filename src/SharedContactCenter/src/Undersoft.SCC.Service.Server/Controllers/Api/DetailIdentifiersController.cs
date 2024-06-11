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

using Undersoft.SCC.Domain.Entities;

/// <summary>
/// The contact address controller.
/// </summary>
public class DetailIdentifiersController
    : ApiCqrsController<
        long,
        IEntryStore,
        IReportStore,
       Identifier<Detail>,
       Identifier<Contracts.Detail>,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DetailIdentifiersController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public DetailIdentifiersController(IServicer servicer) : base(servicer) { }
}
