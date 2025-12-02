using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Repositories;

namespace Moveo_backend.Rental.Domain.Services;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IVehicleRepository _vehicleRepository;

    public RentalService(IRentalRepository rentalRepository, IVehicleRepository vehicleRepository)
    {
        _rentalRepository = rentalRepository;
        _vehicleRepository = vehicleRepository;
    }

    public Task<IEnumerable<Model.Aggregates.Rental>> GetAllAsync() =>
        _rentalRepository.GetAllAsync();

    public Task<IEnumerable<Model.Aggregates.Rental>> GetActiveAsync() =>
        _rentalRepository.GetActiveAsync();

    public Task<Model.Aggregates.Rental?> GetByIdAsync(Guid id) =>
        _rentalRepository.GetByIdAsync(id);

    public Task<IEnumerable<Model.Aggregates.Rental>> GetByUserIdAsync(int userId) =>
        _rentalRepository.GetByUserIdAsync(userId);

    public Task<bool> IsVehicleCurrentlyRentedAsync(Guid vehicleId) =>
        _rentalRepository.IsVehicleCurrentlyRentedAsync(vehicleId);

    public async Task<Model.Aggregates.Rental> CreateRentalAsync(CreateRentalCommand command)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(command.VehicleId);
        if (vehicle == null)
            throw new InvalidOperationException("Vehicle not found");

        var isRented = await _rentalRepository.IsVehicleCurrentlyRentedAsync(command.VehicleId);
        if (isRented)
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

        await _rentalRepository.AddAsync(rental);
        return rental;
    }

    public async Task<Model.Aggregates.Rental?> UpdateRentalAsync(UpdateRentalCommand command)
    {
        var rental = await _rentalRepository.GetByIdAsync(command.Id);
        if (rental == null) return null;

        rental.UpdateTotalPrice(command.TotalPrice);
        await _rentalRepository.UpdateAsync(rental);
        return rental;
    }

    public async Task<bool> CancelRentalAsync(CancelRentalCommand command)
    {
        var rental = await _rentalRepository.GetByIdAsync(command.Id);
        if (rental == null) return false;

        rental.CancelRental(command.Reason);
        await _rentalRepository.UpdateAsync(rental);
        return true;
    }

    public async Task<bool> FinishRentalAsync(FinishRentalCommand command)
    {
        var rental = await _rentalRepository.GetByIdAsync(command.Id);
        if (rental == null) return false;

        rental.FinishRental();
        await _rentalRepository.UpdateAsync(rental);
        return true;
    }
}
