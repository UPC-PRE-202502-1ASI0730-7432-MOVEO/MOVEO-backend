namespace Moveo_backend.AdventureRoutes.Domain.Model.ValueObjects;

public record Waypoint
{
    public double Lat { get; init; }
    public double Lng { get; init; }
    public string Name { get; init; }

    public Waypoint()
    {
        Name = string.Empty;
    }

    public Waypoint(double lat, double lng, string name)
    {
        Lat = lat;
        Lng = lng;
        Name = name;
    }
}
