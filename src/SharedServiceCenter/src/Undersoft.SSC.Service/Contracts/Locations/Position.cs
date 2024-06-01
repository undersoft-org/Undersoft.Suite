using System.Drawing;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Undersoft.SDK;
using Undersoft.SSC.Domain.Entities;

namespace Undersoft.SSC.Service.Contracts.Locations;

public class Position : DataObject
{
    public string? Place { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }

    public int? Length { get; set; }

    public GeoPoint? GeoPoint { get; set; }

    public Point? Point { get; set; }

    public PointF? PrecisePoint { get; set; }

    public Size? Size { get; set; }

    public int Volume { get; set; }

    public int Block { get; set; }

    public int Sector { get; set; }

    public int Cluster { get; set; }

    public int Level { get; set; }

    public long? LocationId { get; set; }
}
