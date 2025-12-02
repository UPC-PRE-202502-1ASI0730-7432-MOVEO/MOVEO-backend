using System.Text.Json;

namespace Moveo_backend.AdventureRoutes.Interfaces.REST.Resources;

public record AdventureRouteResource(
    int Id,
    string Name,
    string Description,
    string Difficulty,
    double Distance,
    string EstimatedTime,
    List<WaypointResource> Waypoints,
    List<string> Images,
    DateTime CreatedAt,
    bool IsActive
);

public record WaypointResource(double Lat, double Lng, string Name);

public record CreateAdventureRouteResource(
    string Name,
    string Description,
    string Difficulty,
    double Distance = 0,
    string? EstimatedTime = "1 hour",
    List<WaypointResource>? Waypoints = null,
    List<string>? Images = null
);

public record UpdateAdventureRouteResource(
    string Name,
    string Description,
    string Difficulty,
    double Distance = 0,
    string? EstimatedTime = "1 hour",
    List<WaypointResource>? Waypoints = null,
    List<string>? Images = null
);
