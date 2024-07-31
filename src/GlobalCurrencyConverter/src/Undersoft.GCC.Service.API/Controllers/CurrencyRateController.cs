using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GCC.Service.API.Controllers
{
    [AllowAnonymous]
    public class CurrencyRateController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.CurrencyRate,
            Contracts.CurrencyRate,
            ServiceManager
        >
    {
        public CurrencyRateController(IServicer servicer)
            : base(servicer) { }
    }

    [Route($"{StoreRoutes.ApiDataRoute}/CurrencyRate")]
    public class CurrencyRatesController
        : ApiCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.CurrencyRate,
            Contracts.CurrencyRate,
            ServiceManager
        >
    {
        public CurrencyRatesController(IServicer servicer)
            : base(servicer) { }
    }
}
