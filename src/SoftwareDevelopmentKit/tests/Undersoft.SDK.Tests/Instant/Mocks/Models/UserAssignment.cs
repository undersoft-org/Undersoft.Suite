using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Undersoft.SDK.Tests.Instant
{
    [Description(
        "Tabela przechowuj¹ca dane o typach u¿ytkowników, w stosunku z encjami systemu operacyjnego"
    )]
    [Table("UserAssignments", Schema = "Users")]
    public class UserAssignment : Identifiable
    {
        [Description("Identyfikator encji w systemie BS")]
        [Required]
        public Guid EntityId { get; set; }

        [Description(
            "Typ encji - Partner = 1,Facility = 2,Client = 3,FacilityService = 4,Client = 5,ClientContract = 6,User = 7,ClientPlatform = 8,ProductVariant = 9,UserProduct = 10"
        )]
        [Required]
        public Type EntityType { get; set; }

        [Description("Identyfikator encji U¿ytkownik")]
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Description(
            "Typ u¿ytkownika - Employee = 1,Companion = 2,Kid = 3,Senior = 4,Addressee = 5,Manager = 6,AccountManager = 7,Support = 8,Contact = 9,Admin = 10"
        )]
        [Required]
        public Type UserType { get; set; }

        public virtual User User { get; set; }
    }
}
