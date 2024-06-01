using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Accounts;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SSC.Service.Application.Server.Controllers
{
    [AllowAnonymous]
    [Route($"{StoreRoutes.ApiAuthRoute}/Account")]
    public class AccountsController
        : ApiDataRemoteController<long, IAccountStore, Contracts.Account, Contracts.Account, AccountService<Contracts.Account>>
    {
        public AccountsController(IServicer servicer) : base(servicer) { }
    }

    [AllowAnonymous]
    public class AccountController
        : OpenDataRemoteController<long, IAccountStore, Contracts.Account, Contracts.Account, AccountService<Contracts.Account>>
    {
        public AccountController(IServicer servicer) : base(servicer) { }
    }
}
