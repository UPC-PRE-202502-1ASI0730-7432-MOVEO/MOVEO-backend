using Microsoft.AspNetCore.Mvc;
using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Services;
using Moveo_backend.Rental.Interfaces.REST.Resources;
using Moveo_backend.Rental.Interfaces.REST.Transform;

namespace Moveo_backend.Rental.Interfaces.REST;

[ApiController]
[Route("api/[controller]")]
public class RentalController : ControllerBase
{
    private readonly IRentalService _rentalService;

    public RentalController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    // 🔹 GET /api/rental
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rentals = await _rentalService.GetAllAsync();
        var resources = rentals.Select(RentalResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    // 🔹 GET /api/rental/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var rental = await _rentalService.GetByIdAsync(id);
        if (rental == null)
            return NotFound();

        var resource = RentalResourceFromEntityAssembler.ToResourceFromEntity(rental);
        return Ok(resource);
    }

    // 🔹 POST /api/rental
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRentalResource resource)
    {
        var command = new CreateRentalCommand(
            resource.VehicleId,
            resource.RenterId,
            resource.OwnerId,
            new Domain.Model.ValueObjects.DateRange(resource.StartDate, resource.EndDate),
            new Domain.Model.ValueObjects.Money(resource.TotalPrice),
            new Domain.Model.ValueObjects.Location(resource.PickupLocation),
            new Domain.Model.ValueObjects.Location(resource.ReturnLocation),
            resource.Notes
        );

        var rental = await _rentalService.CreateRentalAsync(command);
        var result = RentalResourceFromEntityAssembler.ToResourceFromEntity(rental);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // Dentro de RentalController.cs

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRentalResource resource)
    {
        if (id != resource.Id)
            return BadRequest("ID mismatch between URL and payload.");

        // Convertimos decimal -> Money (VO)
        var money = new Moveo_backend.Rental.Domain.Model.ValueObjects.Money(resource.TotalPrice);

        var command = new Moveo_backend.Rental.Domain.Model.Commands.UpdateRentalCommand(resource.Id, money);
        var updatedRental = await _rentalService.UpdateRentalAsync(command);

        if (updatedRental == null)
            return NotFound();

        var result = RentalResourceFromEntityAssembler.ToResourceFromEntity(updatedRental);
        return Ok(result);
    }


    // 🔹 PUT /api/rental/{id}/cancel
    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, [FromBody] CancelRentalResource resource)
    {
        var command = new CancelRentalCommand(id, resource.Reason);
        var success = await _rentalService.CancelRentalAsync(command);

        if (!success)
            return NotFound();

        return Ok(new { Message = "Rental cancelled successfully." });
    }

    // 🔹 PUT /api/rental/{id}/finish
    [HttpPut("{id:guid}/finish")]
    public async Task<IActionResult> Finish(Guid id)
    {
        var command = new FinishRentalCommand(id);
        var success = await _rentalService.FinishRentalAsync(command);

        if (!success)
            return NotFound();

        return Ok(new { Message = "Rental finished successfully." });
    }

    // 🔹 GET /api/rental/user/{userId}
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var rentals = await _rentalService.GetByUserIdAsync(userId);
        var resources = rentals.Select(RentalResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    // 🔹 GET /api/rental/active
    [HttpGet("active")]
    public async Task<IActionResult> GetActiveRentals()
    {
        var rentals = await _rentalService.GetActiveAsync();
        var resources = rentals.Select(RentalResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
