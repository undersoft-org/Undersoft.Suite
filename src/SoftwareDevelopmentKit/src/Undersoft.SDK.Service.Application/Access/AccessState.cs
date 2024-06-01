using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Undersoft.SDK.Service.Application.Access;

public class AccessState : AuthenticationState
{
    public AccessState(ClaimsPrincipal user) : base(user)
    {
    }
}
