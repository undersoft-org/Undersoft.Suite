using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Undersoft.SDK.Tests.Instant
{
    public class User : Origin
    {
        [Description("Imię")]
        [StringLength(100)]
        public string Name { get; set; }

        [Description("Imię")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Description("Nazwisko")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Description("Email")]
        [StringLength(255)]
        public string Email { get; set; }

        [Description("Numer telefonu")]
        [StringLength(100)]
        public string PayrollNumber { get; set; }

        [Description("Nazwa formularza z którego pochodzi metryczka")]
        [StringLength(200)]
        public string SourceLabel { get; set; }

        [Description(
            "Identyfikator encji w systemie zewnętrznym (dotyczy użytkowników operacyjnych)"
        )]
        public int? ExternalId { get; set; }

        [Description("Object ostatniej operacji")]
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime OperationDate { get; set; }

        public virtual UserProfiles Profiles { get; set; } = new UserProfiles();

        public virtual UserAssignments Assignments { get; set; } = new UserAssignments();

        public Guid? MigratedToUserId { get; set; }

        [ForeignKey(nameof(MigratedToUserId))]
        public virtual User MigratedToUser { get; set; }

        [NotMapped]
        public bool IsArchived
        {
            get { return MigratedToUserId.HasValue; }
        }

        public Guid? PrimaryUserId { get; set; }

        [ForeignKey(nameof(PrimaryUserId))]
        public virtual User PrimaryUser { get; set; }

        public virtual ICollection<User> DependentUsers { get; set; }

        public void SetOperationDate(DateTime operationDate)
        {
            OperationDate = operationDate;
        }
    }
}
