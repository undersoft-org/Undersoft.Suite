// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SVC.Service.Server.Catalogs.Controllers.Open
{
    using Microsoft.AspNetCore.Authorization;
    using Undersoft.SVC.Service.Contracts.Catalogs;

    /// <summary>
    /// The contact controller.
    /// </summary>
    /// <summary>
    /// The contact controller.
    /// </summary>
    [Authorize]
    public class ManufacturerController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Catalogs.Manufacturer,
            Manufacturer,
            ServiceManager
        >
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PatientNodeController"/> class.
        /// </summary>
        /// <param name="servicer">The servicer.</param>
        public ManufacturerController(IServicer servicer)
            : base(servicer) { }
    }
}