using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Query
{
    [DataContract]
    public class SortItem
    {
        [DataMember(Order = 3)]
        public string Direction { get; set; }

        [DataMember(Order = 4)]
        public string Property { get; set; }
    }
}
