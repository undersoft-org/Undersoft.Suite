using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Undersoft.SDK.Service.Server.Accounts.Tokens;

public class AccountTokenOptions : IOptions<AccountTokenOptions>
{
    public byte[] SecurityKey { get; set; } = Convert.FromBase64String("RExTamxkMzlyOXIoKiQjOTg5OHNqZGxrc0tqc2Rsa2o=");
    public string Issuer { get; set; } = "Issuer";
    public string Audience { get; set; } = "Audience";
    public int MinutesToExpire { get; set; } = 45;

    public AccountTokenOptions Value => this;

    public AccountTokenOptions() { }
}
