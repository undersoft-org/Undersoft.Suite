using Microsoft.AspNetCore.Authorization;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Accounts;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Open
{
    [AllowAnonymous]
    public class AccountController
        : OpenDataRemoteController<long, IAccountStore, Contracts.Account, Contracts.Account, AccountService<Contracts.Account>>
    {
        public AccountController(IServicer servicer) : base(servicer) { }
    }
}
