using Moveo_backend.Notifications.Domain.Model.Aggregates;

namespace Moveo_backend.Notifications.Domain.Repositories;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetAllAsync();
    Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId);
    Task<Notification?> GetByIdAsync(int id);
    Task AddAsync(Notification notification);
    Task UpdateAsync(Notification notification);
    Task DeleteAsync(int id);
    Task<int> GetUnreadCountAsync(int userId);
}
