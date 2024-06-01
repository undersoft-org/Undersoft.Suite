using System.Drawing;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Undersoft.SDK;

namespace Undersoft.SSC.Domain.Entities.Locations;

public class Position : DataObject
{
    public string? Place { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }

    public int? Length { get; set; }

    public int? X { get; set; }

    public int? Y { get; set; }

    public int? Z { get; set; }

    public int? Size { get; set; }

    public double? Latitue { get; set; }

    public double? Longitude { get; set; }

    public double? Altitude { get; set; }

    public int Volume { get; set; }

    public int Block { get; set; }

    public int Sector { get; set; }

    public int Cluster { get; set; }

    public int Level { get; set; }

    public long? LocationId { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual Location? Location { get; set; }
}
