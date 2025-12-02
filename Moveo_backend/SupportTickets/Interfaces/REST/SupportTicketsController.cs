using Microsoft.AspNetCore.Mvc;
using Moveo_backend.SupportTickets.Domain.Model.Commands;
using Moveo_backend.SupportTickets.Domain.Services;
using Moveo_backend.SupportTickets.Interfaces.REST.Resources;
using Moveo_backend.SupportTickets.Interfaces.REST.Transform;

namespace Moveo_backend.SupportTickets.Interfaces.REST;

[ApiController]
[Route("api/v1/support-tickets")]
public class SupportTicketsController : ControllerBase
{
    private readonly ISupportTicketService _ticketService;

    public SupportTicketsController(ISupportTicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? userId, [FromQuery] string? status)
    {
        if (userId.HasValue)
        {
            var userTickets = await _ticketService.GetByUserIdAsync(userId.Value);
            return Ok(userTickets.Select(SupportTicketResourceFromEntityAssembler.ToResourceFromEntity));
        }
        
        if (!string.IsNullOrEmpty(status))
        {
            var statusTickets = await _ticketService.GetByStatusAsync(status);
            return Ok(statusTickets.Select(SupportTicketResourceFromEntityAssembler.ToResourceFromEntity));
        }
        
        var tickets = await _ticketService.GetAllAsync();
        return Ok(tickets.Select(SupportTicketResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ticket = await _ticketService.GetByIdAsync(id);
        if (ticket == null) return NotFound();
        return Ok(SupportTicketResourceFromEntityAssembler.ToResourceFromEntity(ticket));
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var tickets = await _ticketService.GetByUserIdAsync(userId);
        return Ok(tickets.Select(SupportTicketResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSupportTicketResource resource)
    {
        var command = new CreateSupportTicketCommand(
            resource.UserId,
            resource.Subject,
            resource.Description,
            resource.Category,
            resource.Priority,
            resource.RelatedRentalId,
            resource.RelatedVehicleId
        );
        var ticket = await _ticketService.CreateAsync(command);
        var result = SupportTicketResourceFromEntityAssembler.ToResourceFromEntity(ticket);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSupportTicketResource resource)
    {
        var command = new UpdateSupportTicketCommand(
            id,
            resource.Subject,
            resource.Description,
            resource.Category,
            resource.Priority
        );
        var ticket = await _ticketService.UpdateAsync(command);
        if (ticket == null) return NotFound();
        return Ok(SupportTicketResourceFromEntityAssembler.ToResourceFromEntity(ticket));
    }

    [HttpPut("{id:int}/assign")]
    public async Task<IActionResult> Assign(int id, [FromBody] AssignTicketResource resource)
    {
        var command = new AssignTicketCommand(id, resource.AdminId);
        var ticket = await _ticketService.AssignAsync(command);
        if (ticket == null) return NotFound();
        return Ok(SupportTicketResourceFromEntityAssembler.ToResourceFromEntity(ticket));
    }

    [HttpPut("{id:int}/resolve")]
    public async Task<IActionResult> Resolve(int id, [FromBody] ResolveTicketResource resource)
    {
        var command = new ResolveTicketCommand(id, resource.Resolution);
        var ticket = await _ticketService.ResolveAsync(command);
        if (ticket == null) return NotFound();
        return Ok(SupportTicketResourceFromEntityAssembler.ToResourceFromEntity(ticket));
    }

    [HttpPut("{id:int}/close")]
    public async Task<IActionResult> Close(int id)
    {
        var command = new CloseTicketCommand(id);
        var ticket = await _ticketService.CloseAsync(command);
        if (ticket == null) return NotFound();
        return Ok(SupportTicketResourceFromEntityAssembler.ToResourceFromEntity(ticket));
    }

    [HttpPut("{id:int}/reopen")]
    public async Task<IActionResult> Reopen(int id)
    {
        var command = new ReopenTicketCommand(id);
        var ticket = await _ticketService.ReopenAsync(command);
        if (ticket == null) return NotFound();
        return Ok(SupportTicketResourceFromEntityAssembler.ToResourceFromEntity(ticket));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _ticketService.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
