using Microsoft.AspNetCore.Identity;

namespace Undersoft.SDK.Service.Server.Accounts.Tokens;

public class AccountChangeEmailTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public AccountChangeEmailTokenProviderOptions()
    {
        Name = "ChangeEmailDataProtectorTokenProvider";
        TokenLifespan = TimeSpan.FromHours(4);
    }
}
