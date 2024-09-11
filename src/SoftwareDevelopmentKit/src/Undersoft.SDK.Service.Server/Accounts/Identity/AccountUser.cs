using Microsoft.AspNetCore.Identity;
using Undersoft.SDK.Service.Access.Identity;

namespace Undersoft.SDK.Service.Server.Accounts.Identity;

public class AccountUser : IdentityUser<long>, IUser, IIdentifiable
{
    public AccountUser() { Id = Unique.NewId; }
    public AccountUser(string email) : base(email) { Email = email; Id = email.UniqueKey64(); }
    public AccountUser(string userName, string email) : base(userName) { Email = email; Id = email.UniqueKey64(); }

    public bool RegistrationCompleted { get; set; }

    public bool IsLockedOut { get; set; }

    public long TypeId { get; set; }
}
