using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Model.Commands;

namespace Moveo_backend.Rental.Domain.Services;

public interface IVehicleCommandService
{
    Task<Vehicle> CreateVehicleAsync(CreateVehicleCommand command);
    Task<Vehicle?> UpdateVehicleAsync(UpdateVehicleCommand command);
    Task<bool> DeleteVehicleAsync(Guid id);
}