using Microsoft.EntityFrameworkCore;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SCC.Service.Infrastructure.Stores
{
    public class AccountStore : AccountStore<IAccountStore, AccountStore>
    {
        public AccountStore(DbContextOptions<AccountStore> options) : base(options) { }
    }
}
