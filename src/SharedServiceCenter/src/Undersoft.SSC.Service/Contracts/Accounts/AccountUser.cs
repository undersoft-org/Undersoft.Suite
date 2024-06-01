using System.Runtime.Serialization;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SSC.Service.Contracts.Accounts;

[DataContract]
public class AccountUser : InnerProxy, IAccountUser
{
    [DataMember(Order = 6)]
    public virtual string? UserName { get; set; }

    [DataMember(Order = 7)]
    public virtual string? NormalizedUserName { get; set; }

    [DataMember(Order = 8)]
    public virtual string? Email { get; set; }

    [DataMember(Order = 9)]
    public virtual string? NormalizedEmail { get; set; }

    [DataMember(Order = 10)]
    public virtual bool EmailConfirmed { get; set; }

    [DataMember(Order = 11)]
    public virtual string? PhoneNumber { get; set; }

    [DataMember(Order = 12)]
    public virtual bool PhoneNumberConfirmed { get; set; }

    [DataMember(Order = 13)]
    public virtual bool TwoFactorEnabled { get; set; }

    [DataMember(Order = 14)]
    public virtual DateTimeOffset? LockoutEnd { get; set; }

    [DataMember(Order = 15)]
    public virtual bool LockoutEnabled { get; set; }

    [DataMember(Order = 16)]
    public virtual int AccessFailedCount { get; set; }

    [DataMember(Order = 17)]
    public bool RegistrationCompleted { get; set; }

    [DataMember(Order = 18)]
    public bool IsLockedOut { get; set; }
}
