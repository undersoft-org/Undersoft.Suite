using Microsoft.EntityFrameworkCore;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Infrastructure
// ********************************************************

using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SVC.Service.Infrastructure.Stores
{
    /// <summary>
    /// The entry store.
    /// </summary>
    public class EntryStore : StoreBase<IEntryStore, EntryStore>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntryStore"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public EntryStore(DbContextOptions<EntryStore> options) : base(options) { }
    }
}