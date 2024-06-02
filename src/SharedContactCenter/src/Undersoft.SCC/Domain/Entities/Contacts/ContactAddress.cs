using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SCC.Domain.Entities.Contacts
{
    public class ContactAddress : Entity
    {
        [VisibleRubric]
        public string? Country { get; set; } = default!;

        [VisibleRubric]
        public string? State { get; set; } = default!;

        [VisibleRubric]
        public string? City { get; set; } = default!;

        [VisibleRubric]
        public string? Postcode { get; set; } = default!;

        [VisibleRubric]
        public string? Street { get; set; } = default!;

        [VisibleRubric]
        public string? Building { get; set; } = default!;

        [VisibleRubric]
        public string? Apartment { get; set; } = default!;

        public string? Notes { get; set; }

        public long? ContactId { get; set; }
        public virtual Contact? Contact { get; set; }
    }
}