using System.Text.Json;
using Moveo_backend.AdventureRoutes.Domain.Model.Aggregates;
using Moveo_backend.AdventureRoutes.Domain.Model.Commands;
using Moveo_backend.AdventureRoutes.Domain.Repositories;

namespace Moveo_backend.AdventureRoutes.Domain.Services;

public class AdventureRouteService : IAdventureRouteService
{
    private readonly IAdventureRouteRepository _repository;

    public AdventureRouteService(IAdventureRouteRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<AdventureRoute>> GetAllAsync() => _repository.GetAllAsync();

    public Task<IEnumerable<AdventureRoute>> GetActiveAsync() => _repository.GetActiveAsync();

    public Task<AdventureRoute?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public async Task<AdventureRoute> CreateAsync(CreateAdventureRouteCommand command)
    {
        var waypointsJson = JsonSerializer.Serialize(command.Waypoints ?? new List<WaypointDto>());
        var imagesJson = JsonSerializer.Serialize(command.Images ?? new List<string>());

        var route = new AdventureRoute(
            command.Name,
            command.Description,
            command.Difficulty,
            command.Distance,
            command.EstimatedTime ?? "1 hour",
            waypointsJson,
            imagesJson
        );

        await _repository.AddAsync(route);
        return route;
    }

    public async Task<AdventureRoute?> UpdateAsync(UpdateAdventureRouteCommand command)
    {
        var route = await _repository.GetByIdAsync(command.Id);
        if (route == null) return null;

        var waypointsJson = JsonSerializer.Serialize(command.Waypoints ?? new List<WaypointDto>());
        var imagesJson = JsonSerializer.Serialize(command.Images ?? new List<string>());

        route.UpdateDetails(
            command.Name,
            command.Description,
            command.Difficulty,
            command.Distance,
            command.EstimatedTime ?? "1 hour",
            waypointsJson,
            imagesJson
        );

        await _repository.UpdateAsync(route);
        return route;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var route = await _repository.GetByIdAsync(id);
        if (route == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}
