using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Repositories;

namespace Moveo_backend.Rental.Domain.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public Task<IEnumerable<Vehicle>> GetAllAsync() => _vehicleRepository.GetAllAsync();

    public Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync() => _vehicleRepository.GetAvailableAsync();

    public Task<Vehicle?> GetByIdAsync(Guid id) => _vehicleRepository.GetByIdAsync(id);

    public Task<IEnumerable<Vehicle>> GetByOwnerIdAsync(int ownerId) => _vehicleRepository.GetByOwnerIdAsync(ownerId);

    public async Task<Vehicle> CreateVehicleAsync(CreateVehicleCommand command)
    {
        var vehicle = new Vehicle(
            command.OwnerId,
            command.Brand,
            command.Model,
            command.Year,
            command.Color,
            command.Transmission,
            command.FuelType,
            command.Seats,
            command.DailyPrice,
            command.DepositAmount,
            command.Location,
            command.Features,
            command.Restrictions,
            command.Photos
        );

        await _vehicleRepository.AddAsync(vehicle);
        return vehicle;
    }

    public async Task<Vehicle?> UpdateVehicleAsync(UpdateVehicleCommand command)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(command.Id);
        if (vehicle == null) return null;

        vehicle.UpdateDetails(
            command.Brand,
            command.Model,
            command.Year,
            command.Color,
            command.Transmission,
            command.FuelType,
            command.Seats,
            command.DailyPrice,
            command.DepositAmount,
            command.Location,
            command.Features,
            command.Restrictions,
            command.Photos
        );

        await _vehicleRepository.UpdateAsync(vehicle);
        return vehicle;
    }

    public async Task UpdateVehicleStatusAsync(Guid id, string status)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle != null)
        {
            vehicle.ChangeStatus(status);
            await _vehicleRepository.UpdateAsync(vehicle);
        }
    }

    public async Task<bool> DeleteVehicleAsync(Guid id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle == null) return false;

        await _vehicleRepository.DeleteAsync(id);
        return true;
    }
}
