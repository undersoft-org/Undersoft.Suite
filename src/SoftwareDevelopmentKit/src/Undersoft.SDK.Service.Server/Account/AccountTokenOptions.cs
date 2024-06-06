using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountTokenOptions : IOptions<AccountTokenOptions>
{
    public byte[] SecurityKey { get; set; } = RandomNumberGenerator.GetBytes(64);
    public string Issuer { get; set; } = "Issuer";
    public string Audience { get; set; } = "Audience";
    public int MinutesToExpire { get; set; } = 45;

    public AccountTokenOptions Value => this;

    public AccountTokenOptions() { }
}
