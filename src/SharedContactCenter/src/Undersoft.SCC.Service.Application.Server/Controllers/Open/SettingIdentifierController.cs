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
/// The contact organization controller.
/// </summary>
public class SettingIdentifierController
   : OpenDataRemoteController<
        long,
        IDataStore,
        Identifier<Setting>,
        Identifier<Setting>,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingIdentifierController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public SettingIdentifierController(IServicer servicer) : base(servicer) { }
}
