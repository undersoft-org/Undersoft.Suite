using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.GCC.Service.API.Controllers
{
    [AllowAnonymous]
    public class CurrencyRateTableController
        : OpenCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.CurrencyRateTable,
            Contracts.CurrencyRateTable,
            ServiceManager
        >
    {
        public CurrencyRateTableController(IServicer servicer)
            : base(servicer) { }
    }

    [Route($"{StoreRoutes.ApiDataRoute}/CurrencyRateTable")]
    public class CurrencyRateTablesController
        : ApiCqrsController<
            long,
            IEntryStore,
            IReportStore,
            Domain.Entities.CurrencyRateTable,
            Contracts.CurrencyRateTable,
            ServiceManager
        >
    {
        public CurrencyRateTablesController(IServicer servicer)
            : base(servicer) { }
    }
}
