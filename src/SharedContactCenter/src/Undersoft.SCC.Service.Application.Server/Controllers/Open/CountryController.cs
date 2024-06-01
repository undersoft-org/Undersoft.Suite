using Microsoft.AspNetCore.Authorization;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Open
{
    [AllowAnonymous]
    public class CountryController
        : OpenCqrsController<
            long,
            IEntryStore, IReportStore,
            Domain.Entities.Country,
            Contracts.Country,
            ServiceManager
        >
    {
        public CountryController(IServicer servicer) : base(servicer) { }
    }
}
