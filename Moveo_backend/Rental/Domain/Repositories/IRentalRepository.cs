using Moveo_backend.Rental.Domain.Model.Aggregates;

namespace Moveo_backend.Rental.Domain.Repositories;

public interface IRentalRepository
{
    Task<Model.Aggregates.Rental?> GetByIdAsync(Guid id);
    Task<IEnumerable<Model.Aggregates.Rental>> GetAllAsync();
    Task<IEnumerable<Model.Aggregates.Rental>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Model.Aggregates.Rental>> GetActiveAsync();

    Task AddAsync(Model.Aggregates.Rental rental);
    Task UpdateAsync(Model.Aggregates.Rental rental);
    Task DeleteAsync(Guid id);

    Task<bool> IsVehicleCurrentlyRentedAsync(int vehicleId);
    
}