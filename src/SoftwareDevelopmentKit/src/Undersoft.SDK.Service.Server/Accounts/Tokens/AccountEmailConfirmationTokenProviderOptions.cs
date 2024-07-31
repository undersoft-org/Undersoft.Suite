using Microsoft.AspNetCore.Identity;

namespace Undersoft.SDK.Service.Server.Accounts.Tokens;

public class AccountEmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public AccountEmailConfirmationTokenProviderOptions()
    {
        Name = "EmailDataProtectorTokenProvider";
        TokenLifespan = TimeSpan.FromHours(4);
    }
}
