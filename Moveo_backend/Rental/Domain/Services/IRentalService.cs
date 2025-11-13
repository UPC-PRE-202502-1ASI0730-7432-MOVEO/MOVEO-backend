using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Model.Commands;

namespace Moveo_backend.Rental.Domain.Services;

public interface IRentalService
{
    Task<Model.Aggregates.Rental?> GetByIdAsync(Guid id);
    Task<IEnumerable<Model.Aggregates.Rental>> GetAllAsync();
    Task<IEnumerable<Model.Aggregates.Rental>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Model.Aggregates.Rental>> GetActiveAsync();

    Task<Model.Aggregates.Rental> CreateRentalAsync(CreateRentalCommand command);
    Task<Model.Aggregates.Rental?> UpdateRentalAsync(UpdateRentalCommand command);
    Task<bool> CancelRentalAsync(CancelRentalCommand command);
    Task<bool> FinishRentalAsync(FinishRentalCommand command);

    Task<bool> IsVehicleCurrentlyRentedAsync(Guid vehicleId);
}
