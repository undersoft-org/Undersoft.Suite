using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text.Json.Serialization;

namespace Undersoft.SSC.Service.Contracts.Locations;

[DataContract]
public class Endpoint : DataObject
{
    [DataMember(Order = 12)]
    public string? Host { get; set; }

    [DataMember(Order = 13)]
    public string? IP { get; set; }

    [DataMember(Order = 14)]
    public int? Port { get; set; }

    [DataMember(Order = 15)]
    public string? URI { get; set; }
}
