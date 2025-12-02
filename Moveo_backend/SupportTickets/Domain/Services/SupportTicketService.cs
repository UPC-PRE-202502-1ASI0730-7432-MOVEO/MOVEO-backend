using Moveo_backend.SupportTickets.Domain.Model.Aggregates;
using Moveo_backend.SupportTickets.Domain.Model.Commands;
using Moveo_backend.SupportTickets.Domain.Repositories;

namespace Moveo_backend.SupportTickets.Domain.Services;

public class SupportTicketService : ISupportTicketService
{
    private readonly ISupportTicketRepository _repository;

    public SupportTicketService(ISupportTicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SupportTicket>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<SupportTicket?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<SupportTicket>> GetByUserIdAsync(int userId)
    {
        return await _repository.GetByUserIdAsync(userId);
    }

    public async Task<IEnumerable<SupportTicket>> GetByStatusAsync(string status)
    {
        return await _repository.GetByStatusAsync(status);
    }

    public async Task<SupportTicket> CreateAsync(CreateSupportTicketCommand command)
    {
        var ticket = new SupportTicket(
            command.UserId,
            command.Subject,
            command.Description,
            command.Category,
            command.Priority,
            command.RelatedRentalId,
            command.RelatedVehicleId
        );
        return await _repository.AddAsync(ticket);
    }

    public async Task<SupportTicket?> UpdateAsync(UpdateSupportTicketCommand command)
    {
        var ticket = await _repository.GetByIdAsync(command.Id);
        if (ticket == null) return null;

        ticket.Update(command.Subject, command.Description, command.Category, command.Priority);
        return await _repository.UpdateAsync(ticket);
    }

    public async Task<SupportTicket?> AssignAsync(AssignTicketCommand command)
    {
        var ticket = await _repository.GetByIdAsync(command.TicketId);
        if (ticket == null) return null;

        ticket.AssignTo(command.AdminId);
        return await _repository.UpdateAsync(ticket);
    }

    public async Task<SupportTicket?> ResolveAsync(ResolveTicketCommand command)
    {
        var ticket = await _repository.GetByIdAsync(command.TicketId);
        if (ticket == null) return null;

        ticket.Resolve(command.Resolution);
        return await _repository.UpdateAsync(ticket);
    }

    public async Task<SupportTicket?> CloseAsync(CloseTicketCommand command)
    {
        var ticket = await _repository.GetByIdAsync(command.TicketId);
        if (ticket == null) return null;

        ticket.Close();
        return await _repository.UpdateAsync(ticket);
    }

    public async Task<SupportTicket?> ReopenAsync(ReopenTicketCommand command)
    {
        var ticket = await _repository.GetByIdAsync(command.TicketId);
        if (ticket == null) return null;

        ticket.Reopen();
        return await _repository.UpdateAsync(ticket);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
