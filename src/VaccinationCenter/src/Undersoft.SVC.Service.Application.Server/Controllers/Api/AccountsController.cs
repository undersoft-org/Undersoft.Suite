using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Application.Server
// ********************************************************

using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Accounts;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SVC.Service.Application.Server.Controllers.Api;

/// <summary>
/// The accounts controller.
/// </summary>
[Route($"{StoreRoutes.ApiAuthRoute}/Account")]
public class AccountsController
    : ApiDataRemoteController<
        long,
        IAccountStore,
        Contracts.Account,
        Contracts.Account,
        AccountService<Contracts.Account>
    >
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountsController"/>
    /// class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public AccountsController(IServicer servicer) : base(servicer) { }
}
