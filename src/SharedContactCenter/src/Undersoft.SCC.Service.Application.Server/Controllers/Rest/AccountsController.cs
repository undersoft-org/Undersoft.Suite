using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Accounts;
using Undersoft.SDK.Service.Server.Controller.Api;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Rest
{
    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiAuthRoute}/Account")]
    public class AccountsController
        : ApiDataRemoteController<long, IAccountStore, Contracts.Account, Contracts.Account, AccountService<Contracts.Account>>
    {
        public AccountsController(IServicer servicer) : base(servicer) { }
    }
}
