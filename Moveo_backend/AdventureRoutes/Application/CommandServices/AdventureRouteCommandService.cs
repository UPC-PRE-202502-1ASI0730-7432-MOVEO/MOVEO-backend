using System.Text.Json;
using Moveo_backend.AdventureRoutes.Domain.Model.Aggregates;
using Moveo_backend.AdventureRoutes.Domain.Model.Commands;
using Moveo_backend.AdventureRoutes.Domain.Repositories;

namespace Moveo_backend.AdventureRoutes.Application.CommandServices;

public class AdventureRouteCommandService
{
    private readonly IAdventureRouteRepository _repository;

    public AdventureRouteCommandService(IAdventureRouteRepository repository)
    {
        _repository = repository;
    }

    public async Task<AdventureRoute> Handle(CreateAdventureRouteCommand command)
    {
        var waypointsJson = JsonSerializer.Serialize(command.Waypoints);
        var imagesJson = JsonSerializer.Serialize(command.Images);

        var route = new AdventureRoute(
            command.Name,
            command.Description,
            command.Difficulty,
            command.Distance,
            command.EstimatedTime,
            waypointsJson,
            imagesJson
        );

        await _repository.AddAsync(route);
        return route;
    }

    public async Task<AdventureRoute?> Handle(UpdateAdventureRouteCommand command)
    {
        var route = await _repository.GetByIdAsync(command.Id);
        if (route == null) return null;

        var waypointsJson = JsonSerializer.Serialize(command.Waypoints);
        var imagesJson = JsonSerializer.Serialize(command.Images);

        route.UpdateDetails(
            command.Name,
            command.Description,
            command.Difficulty,
            command.Distance,
            command.EstimatedTime,
            waypointsJson,
            imagesJson
        );

        await _repository.UpdateAsync(route);
        return route;
    }

    public async Task<bool> HandleDelete(int id)
    {
        var route = await _repository.GetByIdAsync(id);
        if (route == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}
