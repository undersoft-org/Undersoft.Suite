namespace Undersoft.GDC.Domain.Entities
{
    public class Address : SDK.Service.Access.Identity.Address, IEntity
    {
        public virtual Listing<Location>? Locations { get; set; }
    }
}
