﻿using Microsoft.AspNetCore.Authorization;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GCC.Service.API.Controllers
{
    [AllowAnonymous]
    public class CurrencyProviderController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.CurrencyProvider,
            Contracts.CurrencyProvider,
            ServiceManager
        >
    {
        public CurrencyProviderController(IServicer servicer) : base(servicer) { }
    }
}