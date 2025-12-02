using Microsoft.EntityFrameworkCore;
using Moveo_backend.Notifications.Domain.Model.Aggregates;
using Moveo_backend.Notifications.Domain.Repositories;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Moveo_backend.Notifications.Infrastructure.Persistence.EFC.Repository;

public class NotificationRepository : INotificationRepository
{
    private readonly AppDbContext _context;

    public NotificationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Notification>> GetAllAsync() =>
        await _context.Notifications.AsNoTracking().OrderByDescending(n => n.CreatedAt).ToListAsync();

    public async Task<IEnumerable<Notification>> GetByUserIdAsync(int userId) =>
        await _context.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId) =>
        await _context.Notifications
            .Where(n => n.UserId == userId && !n.Read)
            .OrderByDescending(n => n.CreatedAt)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Notification?> GetByIdAsync(int id) =>
        await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);

    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Notification notification)
    {
        _context.Notifications.Update(notification);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification != null)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetUnreadCountAsync(int userId) =>
        await _context.Notifications.CountAsync(n => n.UserId == userId && !n.Read);
}
