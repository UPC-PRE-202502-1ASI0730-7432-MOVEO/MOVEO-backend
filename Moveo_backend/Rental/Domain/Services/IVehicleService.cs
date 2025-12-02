using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Model.Commands;

namespace Moveo_backend.Rental.Domain.Services;

public interface IVehicleService
{
    Task<Vehicle?> GetByIdAsync(Guid id);
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync();
    Task<IEnumerable<Vehicle>> GetByOwnerIdAsync(int ownerId);

    Task<Vehicle> CreateVehicleAsync(CreateVehicleCommand command);
    Task<Vehicle?> UpdateVehicleAsync(UpdateVehicleCommand command);
    Task UpdateVehicleStatusAsync(Guid id, string status);
    Task<bool> DeleteVehicleAsync(Guid id);
}