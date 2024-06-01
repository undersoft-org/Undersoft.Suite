using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Operation;
using Undersoft.SDK.Updating;

namespace Undersoft.SDK.Service.Access
{
    [DataContract]
    public class Authorization : DataObject, IAuthorization
    {
        [NotMapped]
        [DataMember(Order = 16)]
        public virtual Credentials Credentials { get; set; } = new Credentials();

        [NotMapped]
        [DataMember(Order = 17)]
        public virtual OperationNotes Notes { get; set; } = new OperationNotes();

        [DataMember(Order = 18)]
        public virtual bool IsAvailable { get; set; }

        [DataMember(Order = 19)]
        public virtual bool Authenticated { get; set; }

        public virtual void Map(object user)
        {
            this.PatchFrom(user);
        }
    }
}
