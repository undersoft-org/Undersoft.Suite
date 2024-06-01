using System.Security.Claims;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Server.Accounts
{
    public interface IAccount : IOrigin, IAuthorization
    {
        long UserId { get; set; }

        AccountUser User { get; set; }

        Listing<Role> Roles { get; set; }

        Listing<AccountClaim> Claims { get; set; }

        IEnumerable<Claim> GetClaims();

        long? PersonalId { get; set; }
        AccountPersonal Personal { get; set; }

        long? AddressId { get; set; }
        AccountAddress Address { get; set; }

        long? ProfessionalId { get; set; }
        AccountProfessional Professional { get; set; }

        long? OrganizationId { get; set; }
        AccountOrganization Organization { get; set; }

        long? SubscriptionId { get; set; }
        AccountSubscription Subscription { get; set; }

        long? PaymentId { get; set; }
        AccountPayment Payment { get; set; }

        long? ConsentId { get; set; }
        AccountConsent Consent { get; set; }

        bool IsAvailable { get; set; }

        bool Authenticated { get; set; }
    }
}