using Microsoft.EntityFrameworkCore;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Moveo_backend.SupportTickets.Domain.Model.Aggregates;
using Moveo_backend.SupportTickets.Domain.Repositories;

namespace Moveo_backend.SupportTickets.Infrastructure.Persistence.EFC.Repository;

public class SupportTicketRepository : ISupportTicketRepository
{
    private readonly AppDbContext _context;

    public SupportTicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SupportTicket>> GetAllAsync()
    {
        return await _context.SupportTickets.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    public async Task<SupportTicket?> GetByIdAsync(int id)
    {
        return await _context.SupportTickets.FindAsync(id);
    }

    public async Task<IEnumerable<SupportTicket>> GetByUserIdAsync(int userId)
    {
        return await _context.SupportTickets
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<SupportTicket>> GetByStatusAsync(string status)
    {
        return await _context.SupportTickets
            .Where(t => t.Status == status)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<SupportTicket>> GetAssignedToAsync(int adminId)
    {
        return await _context.SupportTickets
            .Where(t => t.AssignedToId == adminId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<SupportTicket> AddAsync(SupportTicket ticket)
    {
        await _context.SupportTickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<SupportTicket> UpdateAsync(SupportTicket ticket)
    {
        _context.SupportTickets.Update(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ticket = await _context.SupportTickets.FindAsync(id);
        if (ticket == null) return false;
        
        _context.SupportTickets.Remove(ticket);
        await _context.SaveChangesAsync();
        return true;
    }
}
