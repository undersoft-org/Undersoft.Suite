using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountRegistrationProcessTokenProvider<TUser>
                              : DataProtectorTokenProvider<TUser> where TUser : class
{
    public AccountRegistrationProcessTokenProvider(
        IDataProtectionProvider dataProtectionProvider,
        IOptions<AccountRegistrationConfirmationTokenProviderOptions> options,
        ILogger<DataProtectorTokenProvider<TUser>> logger)
                                       : base(dataProtectionProvider, options, logger)
    {

    }
}
