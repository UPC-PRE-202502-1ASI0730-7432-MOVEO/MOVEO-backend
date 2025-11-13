using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Model.ValueObjects;
using Moveo_backend.Rental.Domain.Services;

namespace Moveo_backend.Rental.Domain.Services;

public class RentalService : IRentalService
{
    private readonly List<Model.Aggregates.Rental> _rentals = new();
    private readonly IVehicleService _vehicleService; // para validaciones

    public RentalService(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public Task<IEnumerable<Model.Aggregates.Rental>> GetAllAsync() => Task.FromResult(_rentals.AsEnumerable());

    public Task<IEnumerable<Model.Aggregates.Rental>> GetActiveAsync() =>
        Task.FromResult(_rentals.Where(r => r.Status == "Active").AsEnumerable());

    public Task<Model.Aggregates.Rental?> GetByIdAsync(Guid id) =>
        Task.FromResult(_rentals.FirstOrDefault(r => r.Id == id));

    public Task<IEnumerable<Model.Aggregates.Rental>> GetByUserIdAsync(Guid userId) =>
        Task.FromResult(_rentals.Where(r => r.RenterId == userId || r.OwnerId == userId).AsEnumerable());

    public Task<bool> IsVehicleCurrentlyRentedAsync(Guid vehicleId) =>
        Task.FromResult(_rentals.Any(r => r.VehicleId == vehicleId && r.Status == "Active"));

    public async Task<Model.Aggregates.Rental> CreateRentalAsync(CreateRentalCommand command)
    {
        var vehicle = await _vehicleService.GetByIdAsync(command.VehicleId);
        if (vehicle == null)
            throw new InvalidOperationException("Vehicle not found");

        if (_rentals.Any(r => r.VehicleId == command.VehicleId && r.Status == "Active"))
            throw new InvalidOperationException("Vehicle is currently rented");

        var rental = new Model.Aggregates.Rental(
            command.VehicleId,
            command.RenterId,
            command.OwnerId,
            command.RentalPeriod,
            command.TotalPrice,
            command.PickupLocation,
            command.ReturnLocation,
            command.Notes
        );

        _rentals.Add(rental);
        return rental;
    }

    public Task<Model.Aggregates.Rental?> UpdateRentalAsync(UpdateRentalCommand command)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == command.Id);
        if (rental == null) return Task.FromResult<Model.Aggregates.Rental?>(null);

        rental.UpdateTotalPrice(command.TotalPrice);
        return Task.FromResult(rental);
    }

    public Task<bool> CancelRentalAsync(CancelRentalCommand command)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == command.Id);
        if (rental == null) return Task.FromResult(false);

        rental.CancelRental(command.Reason);
        return Task.FromResult(true);
    }

    public Task<bool> FinishRentalAsync(FinishRentalCommand command)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == command.Id);
        if (rental == null) return Task.FromResult(false);

        rental.FinishRental();
        return Task.FromResult(true);
    }
}
