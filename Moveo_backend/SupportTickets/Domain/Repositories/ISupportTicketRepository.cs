using Moveo_backend.SupportTickets.Domain.Model.Aggregates;

namespace Moveo_backend.SupportTickets.Domain.Repositories;

public interface ISupportTicketRepository
{
    Task<IEnumerable<SupportTicket>> GetAllAsync();
    Task<SupportTicket?> GetByIdAsync(int id);
    Task<IEnumerable<SupportTicket>> GetByUserIdAsync(int userId);
    Task<IEnumerable<SupportTicket>> GetByStatusAsync(string status);
    Task<IEnumerable<SupportTicket>> GetAssignedToAsync(int adminId);
    Task<SupportTicket> AddAsync(SupportTicket ticket);
    Task<SupportTicket> UpdateAsync(SupportTicket ticket);
    Task<bool> DeleteAsync(int id);
}
