using Microsoft.AspNetCore.Mvc;
using Moveo_backend.Rental.Domain.Services;
using Moveo_backend.Rental.Interfaces.REST.Resources;
using Moveo_backend.Rental.Interfaces.REST.Transform;

namespace Moveo_backend.Rental.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/vehicles")]
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var vehicle = await _vehicleService.GetByIdAsync(id);
        if (vehicle == null) return NotFound();
        return Ok(VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle));
    }

    [HttpGet("owner/{ownerId:int}")]
    public async Task<IActionResult> GetByOwnerId(int ownerId)
    {
        var vehicles = await _vehicleService.GetByOwnerIdAsync(ownerId);
        var resources = vehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable()
    {
        var vehicles = await _vehicleService.GetAvailableVehiclesAsync();
        var resources = vehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehicleResource resource)
    {
        var command = CreateVehicleCommandFromResourceAssembler.ToCommand(resource);
        var vehicle = await _vehicleService.CreateVehicleAsync(command);
        var result = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateVehicleStatusResource resource)
    {
        var vehicle = await _vehicleService.GetByIdAsync(id);
        if (vehicle == null) return NotFound();
        
        await _vehicleService.UpdateVehicleStatusAsync(id, resource.Status);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var vehicle = await _vehicleService.GetByIdAsync(id);
        if (vehicle == null) return NotFound();
        
        await _vehicleService.DeleteVehicleAsync(id);
        return NoContent();
    }
}