﻿using Microsoft.AspNetCore.Authorization;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GCC.Service.API.Controllers
{
    [AllowAnonymous]
    public class CurrencyController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.Currency,
            Contracts.Currency,
            ServiceManager
        >
    {
        public CurrencyController(IServicer servicer) : base(servicer) { }
    }
}