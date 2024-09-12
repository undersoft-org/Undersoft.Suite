using IdentityModel;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.Extensions;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation;
using Undersoft.SDK.Updating;
using Undersoft.SDK.Utilities;
using Claim = System.Security.Claims.Claim;

namespace Undersoft.SDK.Service.Application.Access;

public class AccessProvider<TAccount> : AuthenticationStateProvider, IAccessService<TAccount>, IAccessProvider where TAccount : class, IAuthorization
{
    private readonly IJSRuntime js;
    private IAuthorization _authorization;
    private readonly IRemoteRepository<IAccountStore, TAccount> _repository;
    private readonly string TOKENKEY = "TOKENKEY";
    private readonly string EXPIRATIONTOKENKEY = "EXPIRATIONTOKENKEY";
    private readonly string EMAILKEY = "EMAILKEY";
    private TAccount? _account;
    private AccessState? _accessState;
    private DateTime? accessExpiration;

    private AuthenticationState Anonymous =>
        new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

    public AccessProvider(
        IJSRuntime js,
        IRemoteRepository<IAccountStore, TAccount> repository,
        IAuthorization authorization
    )
    {
        this.js = js;
        _repository = repository;
        _authorization = authorization;
    }

    public IAuthorization Authorization => _authorization;

    public DateTime? AccessExpiration => accessExpiration;

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await js.GetFromLocalStorage(TOKENKEY);
        var email = await js.GetFromLocalStorage(EMAILKEY);

        if (string.IsNullOrEmpty(token))
        {
            return Anonymous;
        }
               
        var expirationTimeString = await js.GetFromLocalStorage(EXPIRATIONTOKENKEY);
        if (expirationTimeString != null)
        {
            DateTimeOffset expirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expirationTimeString));

            if (IsTokenExpired(expirationTime.LocalDateTime))
            {
                await CleanUp();
                return Anonymous;
            }
            if (IsTokenExpired(expirationTime.LocalDateTime.AddMinutes(-5)))
            {
                var auth = await SignedIn(
                    new Authorization()
                    {
                        Credentials = new Credentials() { Email = email, SessionToken = token }
                    }
                );

                if (auth != null)
                {
                    _authorization.Credentials = auth.Credentials;
                    token = auth.Credentials.SessionToken;
                }

                if (token == null)
                    return Anonymous;
            }
        }

        _authorization.Credentials.Email = email;
        return await GetAccessStateAsync(token);
    }

    public async Task<ClaimsPrincipal?> CurrentState()
    {
        if (_accessState == null)
            return (await GetAuthenticationStateAsync()).User;
        return _accessState.User;
    }

    public async Task<ClaimsPrincipal?> RefreshState()
    {
        return (await GetAuthenticationStateAsync()).User;
    }

    public AccessState GetAccessState(string token)
    {
        _authorization.Credentials.SessionToken = token;
        _accessState = new AccessState(
            new ClaimsPrincipal(new ClaimsIdentity(GetTokenClaims(token), "jwt", "name", "role"))
        );
        if (_accessState.User.Identity != null)
            _authorization.Credentials.Authenticated = _accessState.User.Identity.IsAuthenticated;
        if (_authorization.Credentials.Authenticated)
            _repository.Context.SetAuthorization(token);
        return _accessState;
    }

    public async Task<AccessState> GetAccessStateAsync(string token)
    {
        _authorization.Credentials.SessionToken = token;
        await Registered(typeof(TAccount).New<TAccount>());
        _accessState = new AccessState(
            new ClaimsPrincipal(new ClaimsIdentity(GetTokenClaims(token), "jwt", "name", "role"))
        );
        if (_accessState.User.Identity != null)
            _authorization.Credentials.Authenticated = _accessState.User.Identity.IsAuthenticated;
        if (_authorization.Credentials.Authenticated)
            _repository.Context.SetAuthorization(token);
        return _accessState;
    }

    private ISeries<Claim> GetTokenClaims(string jwt)
    {
        var claims = new Registry<Claim>();
        var payload = jwt.Split('.')[1];
        var token = GetJsonToken(payload);

        var keyValuePairs = token.FromJson<Dictionary<string, object>>();
        if (keyValuePairs != null)
            keyValuePairs.ForEach(kvp =>
            {
                claims.Add(kvp.Key, new Claim(kvp.Key, kvp.Value.ToString() ?? ""));
            });

        if (claims.TryGet(JwtClaimTypes.Expiration, out Claim expiration))
        {
            accessExpiration = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiration.Value)).LocalDateTime;
            js.SetInLocalStorage(EXPIRATIONTOKENKEY, expiration.Value);
        }

        return claims;
    }

    private byte[] GetJsonToken(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }
        return Convert.FromBase64String(base64);
    }

    private bool IsTokenExpired(DateTime expirationTime)
    {
        return expirationTime <= DateTime.Now;
    }

    public async Task<IAuthorization?> SignIn(IAuthorization auth)
    {
        var result = await _repository.Access(nameof(SignIn), auth);

        if (result == null)
            return null;

        _authorization.Credentials = result.Credentials;
        _authorization.Notes = result.Notes;
        if (result.Credentials.SessionToken != null)
        {
            await js.SetInLocalStorage(TOKENKEY, result.Credentials.SessionToken);
            await js.SetInLocalStorage(EMAILKEY, result.Credentials.Email);
            var authState = await GetAccessStateAsync(result.Credentials.SessionToken);
            NotifyAuthenticationStateChanged(Task.FromResult((AuthenticationState)authState));
        }
        return _authorization;
    }

    public async Task<IAuthorization> SignUp(IAuthorization auth)
    {
        var result = await _repository.Access(nameof(SignUp), auth);
        _authorization.Credentials = result.Credentials;
        _authorization.Notes = result.Notes;
        return result;
    }

    public async Task<IAuthorization> SignOut(IAuthorization auth)
    {
        auth.Credentials.Email = await js.GetFromLocalStorage(EMAILKEY);

        var result = await _repository.Access(nameof(SignOut), auth);

        await CleanUp();
        NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        return result;
    }

    public async Task<IAuthorization?> SignedIn(IAuthorization auth)
    {
        var result = await _repository.Access(nameof(SignedIn), auth);

        if (result == null)
            return null;

        result.PatchTo(_authorization);
        if (result.Credentials.SessionToken != null)
        {
            await js.SetInLocalStorage(TOKENKEY, result.Credentials.SessionToken);
            await js.SetInLocalStorage(EMAILKEY, result.Credentials.Email);
            var authState = await GetAccessStateAsync(result.Credentials.SessionToken);
            NotifyAuthenticationStateChanged(Task.FromResult((AuthenticationState)authState));
        }
        return _authorization;
    }

    public async Task<IAuthorization?> ResetPassword(IAuthorization auth)
    {
        var result = await _repository.Action(nameof(ResetPassword), auth);

        if (result == null)
            return null;

        return _authorization = result;
    }

    public async Task<IAuthorization?> ChangePassword(IAuthorization auth)
    {
        var result = await _repository.Setup(nameof(ChangePassword), auth);

        if (result == null)
            return null;

        return _authorization = result;
    }

    public async Task<IAuthorization?> SignedUp(IAuthorization auth)
    {
        var result = await _repository.Access(nameof(SignedUp), auth);

        if (result == null)
            return null;

        return result.PatchTo(_authorization);
    }

    public async Task<IAuthorization?> ConfirmEmail(IAuthorization auth)
    {
        var result = await _repository.Action(nameof(ConfirmEmail), auth);

        if (result == null)
            return null;

        return result.PatchTo(_authorization);
    }

    public async Task<IAuthorization?> CompleteRegistration(IAuthorization auth)
    {
        var result = await _repository.Setup(nameof(CompleteRegistration), auth);

        if (result == null)
            return null;

        return result.PatchTo(_authorization);
    }

    public async Task<TAccount> Register(TAccount auth)
    {
        var result = await _repository.Setup(nameof(Register), auth);

        if (result == null)
            return default(TAccount)!;

        ((IAuthorization)result).Credentials.Authenticated = _authorization
            .Credentials
            .Authenticated;
        ((IAuthorization)result).PatchTo(_authorization);

        _account = result;

        return result;
    }

    public async Task<TAccount> Unregister(TAccount auth)
    {
        var result = await _repository.Setup(nameof(Unregister), auth);

        if (result == null)
            return default(TAccount)!;

        ((IAuthorization)result).Credentials.Authenticated = _authorization
            .Credentials
            .Authenticated;
        ((IAuthorization)result).PatchTo(_authorization);
        _account = result;

        return result;
    }

    public async Task<TAccount> Registered(TAccount auth)
    {
        if (_account != null)
            return _account;

        auth.Credentials = _authorization.Credentials;

        var result = await _repository.Access(nameof(Registered), auth);

        if (result == null)
            return default(TAccount)!;

        ((IAuthorization)result).Credentials.Authenticated = _authorization
            .Credentials
            .Authenticated;
        ((IAuthorization)result).PatchTo(_authorization);
        _account = result;

        return result;
    }

    private async Task CleanUp()
    {
        var auth = _authorization;
        await js.RemoveItem(TOKENKEY);
        await js.RemoveItem(EXPIRATIONTOKENKEY);
        await js.RemoveItem(EMAILKEY);
        auth.Notes = new OperationNotes();
        auth.Credentials = new Credentials();
        _repository.Context.SetAuthorization(null);
    }
}
