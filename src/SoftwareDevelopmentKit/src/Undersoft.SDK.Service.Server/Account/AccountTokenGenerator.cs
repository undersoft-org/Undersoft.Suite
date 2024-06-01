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

namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountTokenGenerator
{
    AccountTokenOptions options;
    JwtSecurityTokenHandler handler;
    TokenValidationParameters validationParameters;
    SecurityKey securitykey;

    public AccountTokenGenerator(Action<AccountTokenOptions> builder = null) : this(0, null)
    {
        options = new AccountTokenOptions();
        if (builder != null)
            builder(options);
    }

    public AccountTokenGenerator(int minutesToExpire, AccountTokenOptions jwtOptions = null)
    {
        options = new AccountTokenOptions();
        if (jwtOptions != null)
            options = jwtOptions;
        if (minutesToExpire > 0)
            options.MinutesToExpire = minutesToExpire;

        handler = new JwtSecurityTokenHandler();
        securitykey = new SymmetricSecurityKey(options.SecurityKey);
        validationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.FromMinutes(1),
            ValidIssuer = options.Issuer,
            ValidAudience = options.Audience,
            IssuerSigningKey = securitykey
        };
    }


    public string Generate(IEnumerable<Claim> claims)
    {
        return Generate(
           new ClaimsIdentity(claims)
       );
    }

    public string Generate(Dictionary<string, string> claimsIdentity)
    {
        return Generate(
            new ClaimsIdentity(claimsIdentity.ForEach(kvp => new Claim(kvp.Key, kvp.Value)))
        );
    }

    public string Generate(ClaimsIdentity claimsIdentity)
    {
        var token = handler.CreateJwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            subject: claimsIdentity,
            expires: DateTime.UtcNow.AddMinutes(options.MinutesToExpire),
            signingCredentials: new SigningCredentials(
                securitykey,
                SecurityAlgorithms.HmacSha512Signature,
                SecurityAlgorithms.Sha512Digest
            )
        );

        return handler.WriteToken(token);
    }

    public async Task<TokenValidationResult> Validate(string token)
    {
        return await handler.ValidateTokenAsync(token, validationParameters);
    }
}
