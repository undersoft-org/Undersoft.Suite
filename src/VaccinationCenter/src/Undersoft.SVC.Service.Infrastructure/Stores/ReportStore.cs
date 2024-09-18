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
    /// The report store.
    /// </summary>
    public class ReportStore : StoreBase<IReportStore, ReportStore>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportStore"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ReportStore(DbContextOptions<ReportStore> options) : base(options) { }
    }
}