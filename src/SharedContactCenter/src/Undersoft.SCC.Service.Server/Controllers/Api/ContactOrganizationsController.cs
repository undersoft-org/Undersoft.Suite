// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Server.Controllers.Open;

using Undersoft.SCC.Domain.Entities;

/// <summary>
/// The contact organization controller.
/// </summary>
public class ContactOrganizationsController
    : ApiCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Domain.Entities.Organization,
        Contracts.Organization,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContactOrganizationsController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public ContactOrganizationsController(IServicer servicer) : base(servicer) { }
}
