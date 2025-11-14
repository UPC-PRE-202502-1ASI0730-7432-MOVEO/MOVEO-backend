using Microsoft.AspNetCore.Mvc;
using Moveo_backend.Rental.Domain.Services;
using Moveo_backend.Rental.Interfaces.REST.Resources;
using Moveo_backend.Rental.Interfaces.REST.Transform;
using Moveo_backend.Rental.Domain.Model.ValueObjects;
using Moveo_backend.Rental.Domain.Model.Commands;

namespace Moveo_backend.Rental.Interfaces.REST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vehicles = await _vehicleService.GetAllAsync();
        var resources = vehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehicleResource resource)
    {
        var command = new CreateVehicleCommand(
            resource.OwnerId,
            resource.Brand,
            resource.Model,
            resource.Year,
            resource.Color,
            resource.Transmission,
            resource.FuelType,
            resource.Seats,
            new Money(resource.DailyPrice),
            new Money(resource.DepositAmount),
            new Location(resource.Location),
            resource.Features.ToList(),
            resource.Restrictions.ToList(),
            resource.Photos.ToList()
        );

        var vehicle = await _vehicleService.CreateVehicleAsync(command);
        var result = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }
}