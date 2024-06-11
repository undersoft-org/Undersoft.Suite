// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Server
// ********************************************************

using Undersoft.SCC.Domain.Entities;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Server.Controllers.Open;

/// <summary>
/// The contact address controller.
/// </summary>
public class SettingIdentifierController
    : OpenCqrsController<
        long,
        IEntryStore,
        IReportStore,
        Identifier<Setting>,
        Identifier<Contracts.Setting>,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Address"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public SettingIdentifierController(IServicer servicer) : base(servicer) { }
}
