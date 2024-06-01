using Microsoft.EntityFrameworkCore;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SCC.Service.Infrastructure.Stores
{
    public class ReportStore : StoreBase<IReportStore, ReportStore>
    {
        public ReportStore(DbContextOptions<ReportStore> options) : base(options) { }
    }
}
