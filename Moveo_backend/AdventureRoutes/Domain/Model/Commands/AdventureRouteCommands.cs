namespace Moveo_backend.AdventureRoutes.Domain.Model.Commands;

public record CreateAdventureRouteCommand(
    string Name,
    string Description,
    string Difficulty,
    double Distance = 0,
    string? EstimatedTime = "1 hour",
    List<WaypointDto>? Waypoints = null,
    List<string>? Images = null
);

public record UpdateAdventureRouteCommand(
    int Id,
    string Name,
    string Description,
    string Difficulty,
    double Distance = 0,
    string? EstimatedTime = "1 hour",
    List<WaypointDto>? Waypoints = null,
    List<string>? Images = null
);

public record WaypointDto(double Lat, double Lng, string Name);
