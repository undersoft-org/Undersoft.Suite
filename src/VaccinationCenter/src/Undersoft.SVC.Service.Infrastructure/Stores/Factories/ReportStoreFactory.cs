// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Infrastructure
// ********************************************************

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server;

namespace Undersoft.SVC.Service.Infrastructure.Stores.Factories
{
    /// <summary>
    /// The report store factory.
    /// </summary>
    public class ReportStoreFactory : DbStoreFactory<ReportStore, ServerSourceProviderConfiguration> { }
}