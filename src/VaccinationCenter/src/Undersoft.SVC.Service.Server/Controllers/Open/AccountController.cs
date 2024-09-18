// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Accounts;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SVC.Service.Server.Controllers.Open
{
    /// <summary>
    /// The account controller.
    /// </summary>
    public class AccountController
        : OpenDataController<
            long,
            IAccountStore,
            Account,
            Contracts.Account,
            AccountService<Contracts.Account>
        >
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="servicer">The servicer.</param>
        public AccountController(IServicer servicer) : base(servicer) { }
    }
}