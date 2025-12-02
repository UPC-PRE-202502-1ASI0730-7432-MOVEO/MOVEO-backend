using Moveo_backend.Rental.Domain.Model.Aggregates;

namespace Moveo_backend.Rental.Domain.Repositories;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(Guid id);
    Task<IEnumerable<Vehicle>> GetByOwnerIdAsync(int ownerId);
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<IEnumerable<Vehicle>> GetAvailableAsync(
        string? location = null,
        string? brand = null,
        string? fuelType = null,
        decimal? minPrice = null,
        decimal? maxPrice = null);

    Task AddAsync(Vehicle vehicle);
    Task UpdateAsync(Vehicle vehicle);
    Task DeleteAsync(Guid id);
    
}