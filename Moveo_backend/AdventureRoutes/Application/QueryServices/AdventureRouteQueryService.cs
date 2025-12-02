using Moveo_backend.AdventureRoutes.Domain.Model.Aggregates;
using Moveo_backend.AdventureRoutes.Domain.Repositories;

namespace Moveo_backend.AdventureRoutes.Application.QueryServices;

public class AdventureRouteQueryService
{
    private readonly IAdventureRouteRepository _repository;

    public AdventureRouteQueryService(IAdventureRouteRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<AdventureRoute>> GetAllAsync() => _repository.GetAllAsync();

    public Task<IEnumerable<AdventureRoute>> GetActiveAsync() => _repository.GetActiveAsync();

    public Task<AdventureRoute?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
}
