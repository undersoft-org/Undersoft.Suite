using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Query
{
    [DataContract]
    public class FilterItem
    {
        [DataMember(Order = 3)]
        public string Property { get; set; }

        [DataMember(Order = 4)]
        public string Operand { get; set; }

        [DataMember(Order = 4)]
        public object Value { get; set; }

        [DataMember(Order = 6)]
        public string Logic { get; set; } = "And";
    }
}
