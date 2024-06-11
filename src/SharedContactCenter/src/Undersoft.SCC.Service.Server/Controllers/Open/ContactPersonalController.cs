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

using Undersoft.SCC.Service.Contracts.Contacts;

/// <summary>
/// The contact personal controller.
/// </summary>
public class ContactPersonalController
    : OpenCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Domain.Entities.Contacts.ContactPersonal,
        ContactPersonal,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContactController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public ContactPersonalController(IServicer servicer) : base(servicer) { }
}
