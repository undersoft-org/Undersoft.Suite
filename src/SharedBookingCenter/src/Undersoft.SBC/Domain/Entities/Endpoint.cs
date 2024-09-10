namespace Undersoft.SBC.Domain.Entities
{
    public class Endpoint : SDK.Service.Access.Identity.Endpoint, IEntity
    {
        public virtual long? PlaceId { get; set; }

        public virtual Place? Place { get; set; }
    }
}
