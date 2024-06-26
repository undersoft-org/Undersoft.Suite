using Microsoft.EntityFrameworkCore;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Infrastructure
// ********************************************************

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SVC.Service.Infrastructure.Stores
{
    /// <summary>
    /// The account store.
    /// </summary>
    public class AccountStore : AccountStore<IAccountStore, AccountStore>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountStore"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AccountStore(DbContextOptions<AccountStore> options) : base(options) { }
    }
}