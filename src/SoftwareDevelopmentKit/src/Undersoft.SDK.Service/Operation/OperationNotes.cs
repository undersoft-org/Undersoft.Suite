using System.Runtime.Serialization;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Operation
{
    [DataContract]
    public class OperationNotes : Identifiable
    {
        [DataMember(Order = 0)]
        public string Errors { get; set; }

        [DataMember(Order = 1)]
        public string Success { get; set; }

        [DataMember(Order = 2)]
        public string Info { get; set; }

        [DataMember(Order = 3)]
        public SigningStatus Status { get; set; }

        [DataMember(Order = 4)]
        public bool IsSuccess => Errors != null || (Status & SigningStatus.Failure) > 0 ? false : true;
    }
}
