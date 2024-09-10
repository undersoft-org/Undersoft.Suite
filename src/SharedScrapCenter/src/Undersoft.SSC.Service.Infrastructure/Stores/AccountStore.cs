using Microsoft.EntityFrameworkCore;
using Undersoft.SDK.Service.Server.Accounts;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SSC.Service.Infrastructure.Stores
{
    public class AccountStore : AccountStore<IAccountStore, AccountStore>
    {
        public AccountStore(DbContextOptions<AccountStore> options) : base(options)
        {
        }
    }
}
