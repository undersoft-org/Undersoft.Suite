using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Access.Identity;

public class Place : DataObject
{
    public PlaceKind? Kind { get; set; }

    public string Name { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }

    public int? Length { get; set; }

    public int? X { get; set; }

    public int? Y { get; set; }

    public int? Z { get; set; }

    public int? Size { get; set; }

    public string Unit { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double Altitude { get; set; }

    public int Volume { get; set; }

    public int Block { get; set; }

    public int Sector { get; set; }

    public int Cluster { get; set; }

    public int Level { get; set; }
}
