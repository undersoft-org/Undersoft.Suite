using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Undersoft.GDC.Domain.Entities
{
    public class Place : SDK.Service.Access.Identity.Place, IEntity
    {
        public virtual Listing<Endpoint>? Endpoints { get; set; }

        public long? LocationId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Location? Location { get; set; }
    }
}
