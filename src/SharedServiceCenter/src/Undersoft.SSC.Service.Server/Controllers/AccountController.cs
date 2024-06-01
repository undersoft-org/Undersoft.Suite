using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Accounts;
using Undersoft.SDK.Service.Server.Controller.Api;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SSC.Service.Server.Controllers
{
    [AllowAnonymous]
    public class AccountController
        : OpenDataController<long, IAccountStore, Account, Contracts.Account, AccountService<Contracts.Account>>
    {
        public AccountController(IServicer ultimatr) : base(ultimatr) { }
    }

    [Route($"{StoreRoutes.ApiAuthRoute}/Account")]
    public class AccountsController
        : ApiDataController<long, IAccountStore, Account, Contracts.Account, AccountService<Contracts.Account>>
    {
        public AccountsController(IServicer ultimatr) : base(ultimatr) { }
    }
}
