using System.ServiceModel;

namespace Undersoft.SDK.Service.Data.Response;

using System.Runtime.Serialization;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Query;

[DataContract]
public class ResultString : Identifiable
{
    public ResultString() { }

    public ResultString(string value)
    {
        Value = value;
    }

    [DataMember(Order = 6)]
    public string Value { get; set; }
}