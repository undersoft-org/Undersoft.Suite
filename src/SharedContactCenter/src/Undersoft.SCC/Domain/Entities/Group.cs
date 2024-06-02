namespace Undersoft.SCC.Domain.Entities
{
    public partial class Group : Entity
    {
        public string? Name { get; set; }

        public string? GroupImage { get; set; } = default!;

        public byte[]? GroupImageData { get; set; } = default!;

        public virtual EntitySet<Contact>? Contacts { get; set; }
    }
}
