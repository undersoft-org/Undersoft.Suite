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

/// <summary>
/// The country controller.
/// </summary>
public class CountryController
    : OpenDataRemoteController<
        long,
        IDataStore,
        Contracts.Country,
        Contracts.Country,
        ServiceManager
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CountryController"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public CountryController(IServicer servicer) : base(servicer) { }
}
