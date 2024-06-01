using Microsoft.EntityFrameworkCore;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.GCC.Infrastructure.DataStores
{
    public class ReportStore : StoreBase<IReportStore, ReportStore>
    {
        public ReportStore(DbContextOptions<ReportStore> options) : base(options) { }
    }
}
