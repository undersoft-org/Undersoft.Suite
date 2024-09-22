using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Operation;
using Undersoft.SDK.Service.Server.Accounts.Email;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountService<TAccount> : IAccessService<TAccount>
    where TAccount : class, IOrigin, IAuthorization
{
    private IServicer _servicer;
    private IAccountManager _manager;
    private IEmailSender _email;
    private string _signUpRole = "User";
    private int _openingCount = -1;

    private static ISeries<string> TokenRegistry = new Registry<string>();

    public AccountService() { }

    public AccountService(IServicer servicer, IAccountManager accountManager, IEmailSender email)
    {
        _servicer = servicer;
        _manager = accountManager;
        _email = email;
        HandlePrimeAccount();
    }

    public async Task<IAuthorization> SignIn(IAuthorization identity)
    {
        var account = await ConfirmEmail(await Authenticate(identity));

        if (account.Credentials.Authenticated && account.Credentials.EmailConfirmed)
        {
            account.Credentials.SessionToken = await _manager.GetToken(account);
            account.Notes = new OperationNotes()
            {
                Success = "Signed in",
                Status = AccessStatus.SignedIn
            };
            var _account = await _manager.GetByEmail(account.Credentials.Email);
            var claims = await _manager.User.GetClaimsAsync(_account.User);
            await _manager.SignIn.SignInWithClaimsAsync(
                _account.User,
                account.Credentials.SaveAccountInCookies,
                claims
            );
            var a = typeof(TAccount).New<TAccount>();
            account.Credentials.PatchTo(a.Credentials);
            dynamic registered = await Registered(a);
            if (registered != null && registered.Personal != null)
                ((object)registered.Personal).PatchTo(account.Credentials);
        }
        return account;
    }

    public async Task<IAuthorization> SignUp(IAuthorization identity)
    {
        var _creds = identity.Credentials;
        if (!_manager.TryGetByEmail(_creds.Email, out var account))
        {
            account = await _manager.SetUser(
                _creds.UserName,
                _creds.Email,
                _creds.Password,
                new string[] { _signUpRole }
            );
            if (!account.Notes.IsSuccess)
            {
                identity.Notes = account.Notes;
                return identity;
            }
        }
        else
        {
            identity.Notes = new OperationNotes()
            {
                Errors = "Account already exists!!",
                Status = AccessStatus.Failure
            };
            return identity;
        }

        return await ConfirmEmail(await Authenticate(identity));
    }

    public async Task<IAuthorization> SignOut(IAuthorization identity)
    {
        var account = await SignedUp(identity);

        if (account.Credentials.IsLockedOut)
        {
            var principal = await _manager.SignIn.CreateUserPrincipalAsync(
                await _manager.User.FindByEmailAsync(account.Credentials.Email)
            );
            if (_manager.SignIn.IsSignedIn(principal))
                await _manager.SignIn.SignOutAsync();
            account.Notes = new OperationNotes()
            {
                Success = "Signed out",
                Status = AccessStatus.SignedOut
            };
        }
        return account;
    }

    public async Task<IAuthorization> SignedIn(IAuthorization identity)
    {
        var account = await SignedUp(identity);

        if (!account.Credentials.IsLockedOut)
        {
            var token = await _manager.RenewToken(identity.Credentials.SessionToken);
            if (token != null)
            {
                account.Credentials.SessionToken = token;
                account.Notes = new OperationNotes()
                {
                    Success = "Token renewed",
                    Status = AccessStatus.Succeed
                };
            }
            else
            {
                account.Credentials.SessionToken = null;
                account.Notes = new OperationNotes()
                {
                    Errors = "Invalid token ",
                    Status = AccessStatus.Failure
                };
            }
        }
        return account;
    }

    public async Task<IAuthorization> SignedUp(IAuthorization identity)
    {
        var _creds = identity.Credentials;
        if (!_manager.TryGetByEmail(_creds.Email, out var account))
        {
            _creds.Password = null;
            _creds.IsLockedOut = false;
            _creds.Authenticated = false;
            _creds.EmailConfirmed = false;
            _creds.PhoneNumberConfirmed = false;
            _creds.RegistrationCompleted = false;
            account = new Account() { Credentials = _creds };
            account.Notes = new OperationNotes()
            {
                Errors = "Invalid email",
                Status = AccessStatus.InvalidEmail
            };
            return account;
        }
        var creds = account.Credentials;
        creds.PatchFrom(_creds);
        if (account.User.LockoutEnabled)
            creds.IsLockedOut = account.User.IsLockedOut;
        else
            creds.IsLockedOut = false;
        creds.Authenticated = false;
        creds.EmailConfirmed = account.User.EmailConfirmed;
        creds.PhoneNumberConfirmed = account.User.PhoneNumberConfirmed;
        creds.RegistrationCompleted = account.User.RegistrationCompleted;
        return await Task.FromResult(account);
    }

    public async Task<IAuthorization> Authenticate(IAuthorization account)
    {
        account = await SignedUp(account);

        var _creds = account?.Credentials;

        if (account.Notes.Status == AccessStatus.InvalidEmail)
        {
            _creds.Password = null;
            return account;
        }

        if (!_creds.IsLockedOut)
        {
            if (await _manager.CheckPassword(_creds.Email, _creds.Password) == null)
            {
                _creds.Authenticated = false;
                account.Notes = new OperationNotes()
                {
                    Errors = "Invalid password",
                    Status = AccessStatus.InvalidPassword
                };
            }
            else
            {
                _creds.Authenticated = true;
                account.Notes = new OperationNotes() { Info = "Pasword is valid", };
            }
        }
        else
        {
            _creds.Authenticated = false;
            account.Notes = new OperationNotes()
            {
                Errors = "Account is locked out",
                Status = AccessStatus.InvalidPassword
            };
        }
        _creds.Password = null;
        return account;
    }

    public async Task<IAuthorization> ConfirmEmail(IAuthorization account)
    {
        if (
            account != null
            && !account.Credentials.IsLockedOut
            && account.Credentials.Authenticated
        )
        {
            var _creds = account.Credentials;
            if (!_creds.EmailConfirmed)
            {
                if (_creds.EmailConfirmationToken != null)
                {
                    var _code = int.Parse(_creds.EmailConfirmationToken);
                    var _token = TokenRegistry.Get(_code);
                    var result = await _manager.User.ConfirmEmailAsync(
                        (await _manager.GetByEmail(_creds.Email)).User,
                        _token
                    );
                    TokenRegistry.Remove(_code);
                    if (result.Succeeded)
                    {
                        HandlePrimeAccount();
                        _creds.EmailConfirmed = true;
                        account.Credentials.Authenticated = true;
                        account.Notes = new OperationNotes()
                        {
                            Success = "Email has been confirmed",
                            Status = AccessStatus.EmailConfirmed
                        };
                        this.Success<Accesslog>(account.Notes.Success, account);
                    }
                    else
                    {
                        account.Notes = new OperationNotes()
                        {
                            Errors = result
                                .Errors.Select(d => d.Description)
                                .Aggregate((a, b) => a + ". " + b),
                            Status = AccessStatus.Failure
                        };
                        this.Failure<Accesslog>(account.Notes.Errors, account);
                    }
                    _creds.EmailConfirmationToken = null;
                    return account;
                }

                var token = await _manager.User.GenerateEmailConfirmationTokenAsync(
                    (await _manager.GetByEmail(_creds.Email)).User
                );
                var code = Math.Abs(token.UniqueKey32());
                TokenRegistry.Add(code, token);
                var sender = _servicer.GetService<IEmailSender>();
                await sender.SendEmailAsync(
                    _creds.Email,
                    "Verfication code to confirm your email address and proceed with account registration process",
                    EmailTemplate.GetVerificationCodeMessage(code.ToString())
                );

                account.Notes = new OperationNotes()
                {
                    Info = "Please check your email",
                    Status = AccessStatus.EmailNotConfirmed,
                };
                this.Security<Accesslog>(account.Notes.Info, account);
            }
            else
            {
                account.Notes = new OperationNotes() { Info = "Email was confirmed" };
                account.Credentials.Authenticated = true;
            }
        }
        return account;
    }

    public async Task<IAuthorization> ResetPassword(IAuthorization account)
    {
        account = await SignedUp(account);

        if (account != null && !account.Credentials.IsLockedOut)
        {
            var _creds = account.Credentials;
            if (_creds.PasswordResetToken != null)
            {
                IdentityResult result = null;
                var newpassword = GenerateRandomPassword();
                var _code = int.Parse(_creds.PasswordResetToken);
                var _token = TokenRegistry.Get(_code);
                if (_token != null)
                {
                    result = await _manager.User.ResetPasswordAsync(
                        (await _manager.GetByEmail(_creds.Email)).User,
                        _token,
                        newpassword
                    );
                }
                TokenRegistry.Remove(_code);
                if (result != null && result.Succeeded)
                {
                    account.Credentials.Authenticated = true;
                    account.Notes = new OperationNotes()
                    {
                        Success = "Password has been reset",
                        Status = AccessStatus.ResetPasswordConfirmed
                    };
                    this.Success<Accesslog>(account.Notes.Success, account);
                    _ = _servicer.Serve<IEmailSender>(e =>
                        e.SendEmailAsync(
                            _creds.Email,
                            "Password reset succeed. Now You can sign in, using generated password inside this message. Then change it from the profile settings",
                            EmailTemplate.GetResetPasswordMessage(newpassword)
                        )
                    );
                }
                else
                {
                    account.Credentials.Authenticated = false;
                    account.Notes = new OperationNotes()
                    {
                        Errors = result
                            .Errors.Select(d => d.Description)
                            .Aggregate((a, b) => a + ". " + b),
                        Status = AccessStatus.Failure
                    };
                    this.Failure<Accesslog>(account.Notes.Errors, account);
                }
                _creds.PasswordResetToken = null;
                return account;
            }

            var token = await _manager.User.GeneratePasswordResetTokenAsync(
                (await _manager.GetByEmail(_creds.Email)).User
            );
            var code = Math.Abs(token.UniqueKey32());
            TokenRegistry.Add(code, token);
            _ = _servicer.Serve<IEmailSender>(e =>
                e.SendEmailAsync(
                    _creds.Email,
                    "Verfication code to confirm your decision about resetting the password and sending generated one to your email",
                    EmailTemplate.GetVerificationCodeMessage(code.ToString())
                )
            );

            account.Notes = new OperationNotes()
            {
                Info = "Please check your email to confirm password reset",
                Status = AccessStatus.ResetPasswordNotConfirmed,
            };
            account.Credentials.Authenticated = false;
            this.Security<Accesslog>(account.Notes.Info, account);
        }
        return account;
    }

    public async Task<IAuthorization> ChangePassword(IAuthorization account)
    {
        account = await Authenticate(account);

        if (account != null && account.Credentials.Authenticated)
        {
            var _creds = account.Credentials;
            var result = await _manager.User.ChangePasswordAsync(
                (await _manager.GetByEmail(_creds.Email)).User,
                _creds.Password,
                _creds.NewPassword
            );

            if (result.Succeeded)
            {
                account.Notes = new OperationNotes()
                {
                    Success = "Password has been changed",
                    Status = AccessStatus.Succeed
                };
                this.Success<Accesslog>(account.Notes.Success, account);
            }
            else
            {
                account.Credentials.Authenticated = false;
                account.Notes = new OperationNotes()
                {
                    Errors = result
                        .Errors.Select(d => d.Description)
                        .Aggregate((a, b) => a + ". " + b),
                    Status = AccessStatus.Failure
                };
                this.Failure<Accesslog>(account.Notes.Errors, account);
            }
            _creds.Password = null;
            return account;
        }
        return account;
    }

    public async Task<IAuthorization> CompleteRegistration(IAuthorization account)
    {
        var _creds = account.Credentials;
        if (!_creds.RegistrationCompleted)
        {
            var _account = await _manager.GetByEmail(_creds.Email);
            if (_account == null)
            {
                account.Notes = new OperationNotes()
                {
                    Errors = "Account not found",
                    Status = AccessStatus.RegistrationNotCompleted
                };
                this.Failure<Accesslog>(account.Notes.Success, account);
                return account;
            }

            if (_creds.RegistrationCompleteToken != null)
            {
                var _code = int.Parse(_creds.RegistrationCompleteToken);
                var _token = TokenRegistry.Get(_code);
                TokenRegistry.Remove(_code);
                var isValid = await _manager.User.VerifyUserTokenAsync(
                    _account.User,
                    "AccountRegistrationProcessTokenProvider",
                    "Registration",
                    _token
                );

                if (isValid)
                {
                    _account.User.RegistrationCompleted = true;

                    if ((await _manager.User.UpdateAsync(_account.User)).Succeeded)
                    {
                        _creds.RegistrationCompleted = true;
                        _creds.Authenticated = true;
                        account.Notes = new OperationNotes()
                        {
                            Success = "Registration completed",
                            Status = AccessStatus.RegistrationCompleted
                        };
                        this.Success<Accesslog>(account.Notes.Success, account);
                    }
                    else
                    {
                        this.Failure<Accesslog>(account.Notes.Errors, account);
                    }
                }
                else
                {
                    account.Notes = new OperationNotes()
                    {
                        Errors = "Registration not completed. Invalid verification code",
                        Status = AccessStatus.RegistrationNotCompleted
                    };
                    this.Failure<Accesslog>(account.Notes.Success, account);
                }

                _creds.RegistrationCompleteToken = null;
                return account;
            }

            var token = await _manager.User.GenerateUserTokenAsync(
                (await _manager.GetByEmail(_creds.Email)).User,
                "AccountRegistrationProcessTokenProvider",
                "Registration"
            );
            var code = Math.Abs(token.UniqueKey32());
            TokenRegistry.Add(code, token);
            _ = _servicer.Serve<IEmailSender>(e =>
                e.SendEmailAsync(
                    _creds.Email,
                    "Verfication code to confirm your email address and proceed with account registration process",
                    EmailTemplate.GetVerificationCodeMessage(code.ToString())
                )
            );
            account.Notes = new OperationNotes()
            {
                Info = "Please confirm registration process",
                Status = AccessStatus.RegistrationNotConfirmed
            };
        }
        else
            account.Notes = new OperationNotes() { Info = "Registration was completed" };

        return account;
    }

    public async Task<TAccount> Register(TAccount account)
    {
        var _creds = account.Credentials;
        var _account = await _manager.GetByEmail(_creds.Email);

        if (_account == null)
        {
            account.Notes = new OperationNotes()
            {
                Errors = "Account not found",
                Status = AccessStatus.RegistrationNotCompleted
            };
            this.Failure<Accesslog>(account.Notes.Success, account);
            return account;
        }

        account.PutTo(_account);

        var _accountuser = await _manager.User.FindByEmailAsync(_creds.Email);

        if (account.Notes.Status != AccessStatus.RegistrationNotCompleted)
        {
            _accountuser.RegistrationCompleted = true;
            var claims = _manager.User.AddClaimsAsync(
                    _accountuser, new[] {

                        new Claim("tenant_id", _account.Tenant.Id.ToString()),
                        new Claim("organization_id", _account.Organization.Id.ToString())
                    }
                );
            claims.Wait();

        }
        if ((await _manager.User.UpdateAsync(_accountuser)).Succeeded)
        {
            if (account.Notes.Status != AccessStatus.RegistrationNotCompleted)
            {                
                _creds.RegistrationCompleted = true;
            }
            _creds.Authenticated = true;
            _account.Notes = new OperationNotes()
            {
                Success = "Registration completed",
                Status = AccessStatus.RegistrationCompleted
            };
            this.Success<Accesslog>(_account.Notes.Success, account);
        }
        else
        {
            this.Failure<Accesslog>(_account.Notes.Errors, account);
        }

        _account = await _manager.Accounts.Put(_account, null);

        var count = await _manager.Accounts.Save(true);

        _account.PatchTo(account);
        _accountuser.PatchTo(account.Credentials);
        _account.Personal.PatchTo(account.Credentials);

        return account;
    }

    public async Task<TAccount> Unregister(TAccount account)
    {
        var _creds = account.Credentials;
        var _account = await _manager.GetByEmail(_creds.Email);

        if (_account == null)
        {
            account.Notes = new OperationNotes()
            {
                Errors = "Account not found",
                Status = AccessStatus.RegistrationNotCompleted
            };
            this.Failure<Accesslog>(account.Notes.Success, account);
            return account;
        }

        var _accountuser = await _manager.User.FindByEmailAsync(_creds.Email);
        if (_accountuser != null)
        {
            _account = await _manager.Accounts.Delete(_accountuser.Id);
            if (_account != null)
            {
                _account.User = _accountuser;
                _account.PatchTo(account);
                _accountuser.PatchTo(account.Credentials);
                _account.Personal.PatchTo(account.Credentials);
            }
        }
        return account;
    }

    public async Task<TAccount> Registered(TAccount account)
    {
        var _creds = account.Credentials;
        var _account = await _manager.GetByEmail(_creds.Email);

        if (_account == null)
        {
            account.Notes = new OperationNotes()
            {
                Errors = "Account not found",
                Status = AccessStatus.RegistrationNotCompleted
            };
            this.Failure<Accesslog>(account.Notes.Success, account);
            return account;
        }

        var _accountuser = await _manager.User.FindByEmailAsync(_creds.Email);
        if (_accountuser != null)
        {
            _account = _manager.Accounts.Query.Where(a => a.Id == _accountuser.Id).FirstOrDefault();
            if (_account != null)
            {
                _account.User = _accountuser;
                _account.PutTo(account);               
                _accountuser.PatchTo(account.Credentials);
                _account.Personal.PatchTo(account.Credentials);
            }
        }
        return account;
    }

    public Task<ClaimsPrincipal?> RefreshAsync()
    {
        throw new Exception("Account service doesn't provide current state");
    }

    private int Count()
    {
        return _manager.Accounts.Query.Count();
    }

    private void HandlePrimeAccount()
    {
        if (_openingCount < 0)
            _openingCount = Count();
        if (_openingCount == 0)
            _signUpRole = "Administrator";
        else
            _signUpRole = "User";
    }

    public static string GenerateRandomPassword(PasswordOptions opts = null)
    {
        if (opts == null)
            opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

        string[] randomChars = new[]
        {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ", // uppercase
            "abcdefghijkmnopqrstuvwxyz", // lowercase
            "0123456789", // digits
            "!@$?_-" // non-alphanumeric
        };

        Random rand = new Random(Environment.TickCount);
        List<char> chars = new List<char>();

        if (opts.RequireUppercase)
            chars.Insert(
                rand.Next(0, chars.Count),
                randomChars[0][rand.Next(0, randomChars[0].Length)]
            );

        if (opts.RequireLowercase)
            chars.Insert(
                rand.Next(0, chars.Count),
                randomChars[1][rand.Next(0, randomChars[1].Length)]
            );

        if (opts.RequireDigit)
            chars.Insert(
                rand.Next(0, chars.Count),
                randomChars[2][rand.Next(0, randomChars[2].Length)]
            );

        if (opts.RequireNonAlphanumeric)
            chars.Insert(
                rand.Next(0, chars.Count),
                randomChars[3][rand.Next(0, randomChars[3].Length)]
            );

        for (
            int i = chars.Count;
            i < opts.RequiredLength || chars.Distinct().Count() < opts.RequiredUniqueChars;
            i++
        )
        {
            string rcs = randomChars[rand.Next(0, randomChars.Length)];
            chars.Insert(rand.Next(0, chars.Count), rcs[rand.Next(0, rcs.Length)]);
        }

        return new string(chars.ToArray());
    }
}
