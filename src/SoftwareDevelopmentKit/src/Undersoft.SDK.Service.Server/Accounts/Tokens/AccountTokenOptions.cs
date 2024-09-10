using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Undersoft.SDK.Service.Server.Accounts.Tokens;

public class AccountTokenOptions : IOptions<AccountTokenOptions>
{
    public byte[] SecurityKey { get; set; } = Encoding.ASCII.GetBytes("3@#)(2$@$5235dss3$@rwfsdfkpodkpz#$%@#DWODKSPKKWIofedfo93urUWPW3i$#&*()&3op");
    public string Issuer { get; set; } = "Issuer";
    public string Audience { get; set; } = "Audience";
    public int MinutesToExpire { get; set; } = 45;

    public AccountTokenOptions Value => this;

    public AccountTokenOptions() { }
}
