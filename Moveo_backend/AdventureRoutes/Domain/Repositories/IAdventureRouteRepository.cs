using Moveo_backend.AdventureRoutes.Domain.Model.Aggregates;

namespace Moveo_backend.AdventureRoutes.Domain.Repositories;

public interface IAdventureRouteRepository
{
    Task<IEnumerable<AdventureRoute>> GetAllAsync();
    Task<IEnumerable<AdventureRoute>> GetActiveAsync();
    Task<AdventureRoute?> GetByIdAsync(int id);
    Task AddAsync(AdventureRoute route);
    Task UpdateAsync(AdventureRoute route);
    Task DeleteAsync(int id);
}
