using Microsoft.EntityFrameworkCore;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.GCC.Infrastructure.DataStores
{
    public class EntryStore : StoreBase<IEntryStore, EntryStore>
    {
        public EntryStore(DbContextOptions<EntryStore> options) : base(options) { }
    }
}
