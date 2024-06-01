using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountPasswordResetTokenProvider<TUser>
                              : DataProtectorTokenProvider<TUser> where TUser : class
{
    public AccountPasswordResetTokenProvider(
        IDataProtectionProvider dataProtectionProvider,
        IOptions<AccountPasswordResetTokenProviderOptions> options,
        ILogger<DataProtectorTokenProvider<TUser>> logger)
                                       : base(dataProtectionProvider, options, logger)
    {

    }
}
