using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Services;

namespace Moveo_backend.Rental.Application.CommandServices;

public class VehicleCommandService
{
    private readonly IVehicleService _vehicleService;

    public VehicleCommandService(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public Task<Vehicle> Handle(CreateVehicleCommand command)
        => _vehicleService.CreateVehicleAsync(command);

    public Task<Vehicle?> Handle(UpdateVehicleCommand command)
        => _vehicleService.UpdateVehicleAsync(command);

    public Task<bool> HandleDelete(Guid id)
        => _vehicleService.DeleteVehicleAsync(id);
}