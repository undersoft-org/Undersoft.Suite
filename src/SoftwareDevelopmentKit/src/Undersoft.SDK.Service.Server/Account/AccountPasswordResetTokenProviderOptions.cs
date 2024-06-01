using Microsoft.AspNetCore.Identity;

namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountPasswordResetTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public AccountPasswordResetTokenProviderOptions()
    {
        Name = "PasswordResetDataProtectorTokenProvider";
        TokenLifespan = TimeSpan.FromHours(4);
    }
}
