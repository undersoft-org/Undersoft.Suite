using System.Runtime.Serialization;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Access
{
    [Serializable]
    public enum ServiceSite
    {
        Client,
        Server
    }

    public enum DataSite
    {
        None,
        Client,
        Endpoint
    }

    public enum IdentityType
    {
        User,
        Server,
        Service
    }

    [DataContract]
    public class Credentials : DataObject, ICredentials
    {
        [DataMember(Order = 0)]
        public ServiceSite? Site { get; set; }

        [DataMember(Order = 1)]
        public IdentityType? Type { get; set; }

        [RequiredRubric]
        [DataMember(Order = 2)]
        [DisplayRubric("Name")]
        public string UserName { get; set; }

        [DataMember(Order = 3)]
        public string NormalizedUserName { get; set; }

        [RequiredRubric]
        [DataMember(Order = 4)]
        [DisplayRubric("Email")]
        public string Email { get; set; }

        [DataMember(Order = 5)]
        [DisplayRubric("Old Password")]
        public string OldPassword { get; set; }

        [RequiredRubric]
        [DataMember(Order = 6)]
        [DisplayRubric("Password")]
        public string Password { get; set; }

        [RequiredRubric]
        [DataMember(Order = 7)]
        [DisplayRubric("Phone number")]
        public string PhoneNumber { get; set; }

        [DataMember(Order = 8)]
        public bool EmailConfirmed { get; set; }

        [DataMember(Order = 9)]
        public bool PhoneNumberConfirmed { get; set; }

        [DataMember(Order = 10)]
        public bool RegistrationCompleted { get; set; }

        [DataMember(Order = 11)]
        public string SessionToken { get; set; }

        [RequiredRubric]
        [DataMember(Order = 12)]
        [DisplayRubric("Recovery verification code")]
        public string PasswordResetToken { get; set; }

        [RequiredRubric]
        [DataMember(Order = 13)]
        [DisplayRubric("Email verification code")]
        public string EmailConfirmationToken { get; set; }

        [RequiredRubric]
        [DataMember(Order = 14)]
        [DisplayRubric("Phone verification code")]
        public string PhoneNumberConfirmationToken { get; set; }

        [DataMember(Order = 15)]
        public string RegistrationCompleteToken { get; set; }

        [DataMember(Order = 16)]
        public int AccessFailedCount { get; set; }

        [DataMember(Order = 17)]
        [DisplayRubric("Remember me")]
        public bool SaveAccountInCookies { get; set; }

        [DataMember(Order = 18)]
        public bool Authenticated { get; set; }

        [DataMember(Order = 19)]
        public bool IsLockedOut { get; set; }

        [DataMember(Order = 20)]
        public string ReturnPath { get; set; }

        [RequiredRubric]
        [DataMember(Order = 21)]
        [DisplayRubric("Retype password")]
        public string RetypedPassword { get; set; }

        [RequiredRubric]
        [DataMember(Order = 22)]
        [DisplayRubric("First name")]
        public string FirstName { get; set; }

        [RequiredRubric]
        [DataMember(Order = 23)]
        [DisplayRubric("Last name")]
        public string LastName { get; set; }

        [RequiredRubric]
        [DataMember(Order = 24)]
        [DisplayRubric("Terms acceptance")]
        public bool TermsConsent { get; set; }

        [RequiredRubric]
        [DataMember(Order = 25)]
        [DisplayRubric("Terms acceptance")]
        public bool CookiesConsent { get; set; }

        [RequiredRubric]
        [DataMember(Order = 26)]
        [DisplayRubric("Terms acceptance")]
        public bool OptionalConsent { get; set; }

        [RequiredRubric]
        [DataMember(Order = 6)]
        [DisplayRubric("New Password")]
        public string NewPassword { get; set; }

        public string Image { get; set; }

        public byte[] ImageData { get; set; }
    }
}
