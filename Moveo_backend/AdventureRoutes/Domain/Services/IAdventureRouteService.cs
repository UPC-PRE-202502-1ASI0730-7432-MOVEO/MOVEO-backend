using Moveo_backend.AdventureRoutes.Domain.Model.Aggregates;
using Moveo_backend.AdventureRoutes.Domain.Model.Commands;

namespace Moveo_backend.AdventureRoutes.Domain.Services;

public interface IAdventureRouteService
{
    Task<IEnumerable<AdventureRoute>> GetAllAsync();
    Task<IEnumerable<AdventureRoute>> GetActiveAsync();
    Task<AdventureRoute?> GetByIdAsync(int id);
    Task<AdventureRoute> CreateAsync(CreateAdventureRouteCommand command);
    Task<AdventureRoute?> UpdateAsync(UpdateAdventureRouteCommand command);
    Task<bool> DeleteAsync(int id);
}
