using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountChangeEmailTokenProvider<TUser>
                              : DataProtectorTokenProvider<TUser> where TUser : class
{
    public AccountChangeEmailTokenProvider(
        IDataProtectionProvider dataProtectionProvider,
        IOptions<AccountChangeEmailTokenProviderOptions> options,
        ILogger<DataProtectorTokenProvider<TUser>> logger)
                                       : base(dataProtectionProvider, options, logger)
    {

    }
}
