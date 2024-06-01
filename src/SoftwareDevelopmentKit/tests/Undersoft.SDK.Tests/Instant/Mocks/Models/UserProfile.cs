using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Undersoft.SDK.Tests.Instant
{
    public class UserProfile : Origin
    {
        [ForeignKey("User")]
        public Guid? UserId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = default!;

        [StringLength(100)]
        public string Surname { get; set; } = default!;

        public int? ProvinceId { get; set; }

        [StringLength(100)]
        public string City { get; set; } = default!;

        [StringLength(100)]
        public string Street { get; set; } = default!;

        [StringLength(50)]
        public string BuildingNumber { get; set; } = default!;

        [StringLength(50)]
        public string ApartmentNumber { get; set; } = default!;

        [StringLength(6)]
        public string Postcode { get; set; } = default!;

        [StringLength(50)]
        public string PhoneNumber { get; set; } = default!;

        [StringLength(255)]
        public string Email { get; set; } = default!;

        public long ChipNumber { get; set; }

        public string Language { get; set; } = default!;

        [StringLength(100)]
        public string PayrollNumber { get; set; } = default!;

        public int CustomerExternalId { get; set; }

        public string CustomerName { get; set; } = default!;

        public string FacebookId { get; set; } = default!;

        [StringLength(11)]
        public string Pesel { get; set; } = default!;

        public Guid? SSOID { get; set; }

        public User User { get; set; } = default!;
    }
}
