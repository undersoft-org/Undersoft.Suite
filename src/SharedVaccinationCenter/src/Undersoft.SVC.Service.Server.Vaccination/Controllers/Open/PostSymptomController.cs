// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SVC.Service.Server.Vaccination.Controllers.Open
{
    using Undersoft.SVC.Service.Contracts.Vaccination;

    /// <summary>
    /// The contact controller.
    /// </summary>
    public class PostSymptomController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Vaccination.PostSymptom,
            PostSymptom,
            ServiceManager
        >
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostSymptomController"/> class.
        /// </summary>
        /// <param name="servicer">The servicer.</param>
        public PostSymptomController(IServicer servicer) : base(servicer) { }
    }
}