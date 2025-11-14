using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Services;

namespace Moveo_backend.Rental.Application.QueryServices;

public class VehicleQueryService
{
    private readonly IVehicleService _vehicleService;

    public VehicleQueryService(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public Task<IEnumerable<Vehicle>> GetAllAsync() => _vehicleService.GetAllAsync();
    public Task<Vehicle?> GetByIdAsync(Guid id) => _vehicleService.GetByIdAsync(id);
    public Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync() => _vehicleService.GetAvailableVehiclesAsync();
    public Task<IEnumerable<Vehicle>> GetByOwnerIdAsync(Guid ownerId) => _vehicleService.GetByOwnerIdAsync(ownerId);
}