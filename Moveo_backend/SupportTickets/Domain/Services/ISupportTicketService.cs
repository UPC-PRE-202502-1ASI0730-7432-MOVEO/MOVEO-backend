using Moveo_backend.SupportTickets.Domain.Model.Aggregates;
using Moveo_backend.SupportTickets.Domain.Model.Commands;

namespace Moveo_backend.SupportTickets.Domain.Services;

public interface ISupportTicketService
{
    Task<IEnumerable<SupportTicket>> GetAllAsync();
    Task<SupportTicket?> GetByIdAsync(int id);
    Task<IEnumerable<SupportTicket>> GetByUserIdAsync(int userId);
    Task<IEnumerable<SupportTicket>> GetByStatusAsync(string status);
    Task<SupportTicket> CreateAsync(CreateSupportTicketCommand command);
    Task<SupportTicket?> UpdateAsync(UpdateSupportTicketCommand command);
    Task<SupportTicket?> AssignAsync(AssignTicketCommand command);
    Task<SupportTicket?> ResolveAsync(ResolveTicketCommand command);
    Task<SupportTicket?> CloseAsync(CloseTicketCommand command);
    Task<SupportTicket?> ReopenAsync(ReopenTicketCommand command);
    Task<bool> DeleteAsync(int id);
}
