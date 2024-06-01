using Microsoft.EntityFrameworkCore;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SSC.Service.Infrastructure.Stores
{
    public class EntryStore : StoreBase<IEntryStore, EntryStore>
    {
        public EntryStore(DbContextOptions<EntryStore> options) : base(options) { }
    }
}
