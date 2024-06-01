using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using NetTopologySuite.Algorithm;
using Microsoft.Extensions.Options;

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
