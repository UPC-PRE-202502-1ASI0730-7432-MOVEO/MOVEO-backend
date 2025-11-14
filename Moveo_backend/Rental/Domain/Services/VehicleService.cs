using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Services;

namespace Moveo_backend.Rental.Domain.Services;

public class VehicleService : IVehicleService
{
    private readonly List<Vehicle> _vehicles = new();

    public Task<IEnumerable<Vehicle>> GetAllAsync() => Task.FromResult(_vehicles.AsEnumerable());

    public Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync()
    {
        var available = _vehicles.Where(v => v.IsAvailable);
        return Task.FromResult(available);
    }

    public Task<Vehicle?> GetByIdAsync(Guid id) =>
        Task.FromResult(_vehicles.FirstOrDefault(v => v.Id == id));

    public Task<IEnumerable<Vehicle>> GetByOwnerIdAsync(Guid ownerId)
    {
        var vehicles = _vehicles.Where(v => v.OwnerId == ownerId);
        return Task.FromResult(vehicles);
    }

    public Task<Vehicle> CreateVehicleAsync(CreateVehicleCommand command)
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
            command.Features.ToList(),
            command.Restrictions.ToList(),
            command.Photos.ToList()
        );

        _vehicles.Add(vehicle);
        return Task.FromResult(vehicle);
    }

    public Task<Vehicle?> UpdateVehicleAsync(UpdateVehicleCommand command)
    {
        var vehicle = _vehicles.FirstOrDefault(v => v.Id == command.Id);
        if (vehicle == null) return Task.FromResult<Vehicle?>(null);

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
            command.Features.ToList(),
            command.Restrictions.ToList(),
            command.Photos.ToList()
        );

        return Task.FromResult(vehicle);
    }

    public Task<bool> DeleteVehicleAsync(Guid id)
    {
        var vehicle = _vehicles.FirstOrDefault(v => v.Id == id);
        if (vehicle == null) return Task.FromResult(false);

        _vehicles.Remove(vehicle);
        return Task.FromResult(true);
    }
}
