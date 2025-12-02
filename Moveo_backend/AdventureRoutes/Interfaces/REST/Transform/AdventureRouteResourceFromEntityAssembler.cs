using System.Text.Json;
using Moveo_backend.AdventureRoutes.Domain.Model.Aggregates;
using Moveo_backend.AdventureRoutes.Interfaces.REST.Resources;

namespace Moveo_backend.AdventureRoutes.Interfaces.REST.Transform;

public static class AdventureRouteResourceFromEntityAssembler
{
    public static AdventureRouteResource ToResourceFromEntity(AdventureRoute route)
    {
        var waypoints = JsonSerializer.Deserialize<List<WaypointResource>>(route.WaypointsJson) 
            ?? new List<WaypointResource>();
        var images = JsonSerializer.Deserialize<List<string>>(route.ImagesJson) 
            ?? new List<string>();

        return new AdventureRouteResource(
            route.Id,
            route.Name,
            route.Description,
            route.Difficulty,
            route.Distance,
            route.EstimatedTime,
            waypoints,
            images,
            route.CreatedAt,
            route.IsActive
        );
    }
}
