using Moveo_backend.Notifications.Domain.Model.Aggregates;
using Moveo_backend.Notifications.Domain.Model.Commands;

namespace Moveo_backend.Notifications.Domain.Services;

public interface INotificationService
{
    Task<IEnumerable<Notification>> GetAllAsync();
    Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId);
    Task<Notification?> GetByIdAsync(int id);
    Task<Notification> CreateNotificationAsync(CreateNotificationCommand command);
    Task<bool> MarkAsReadAsync(int id);
    Task<bool> MarkAllAsReadAsync(int userId);
    Task<bool> DeleteAsync(int id);
    Task<int> GetUnreadCountAsync(int userId);
}
