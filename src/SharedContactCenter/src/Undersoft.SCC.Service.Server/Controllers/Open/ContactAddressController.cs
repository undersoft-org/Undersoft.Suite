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
/// The contact address controller.
/// </summary>
public class ContactAddressController
    : OpenCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Domain.Entities.Contacts.ContactAddress,
        ContactAddress,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContactAddress"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public ContactAddressController(IServicer servicer) : base(servicer) { }
}
