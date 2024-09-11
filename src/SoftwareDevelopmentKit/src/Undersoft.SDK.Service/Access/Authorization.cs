using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SDK.Service.Access
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public class Authorization : DataObject, IAuthorization
    {
        [DataMember(Order = 16)]
        public virtual Credentials Credentials { get; set; } = new Credentials();

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
