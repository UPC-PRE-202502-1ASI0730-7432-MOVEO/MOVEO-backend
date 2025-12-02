using Moveo_backend.Notifications.Domain.Model.Aggregates;
using Moveo_backend.Notifications.Domain.Model.Commands;
using Moveo_backend.Notifications.Domain.Repositories;

namespace Moveo_backend.Notifications.Domain.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repository;

    public NotificationService(INotificationRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Notification>> GetAllAsync() => _repository.GetAllAsync();

    public Task<IEnumerable<Notification>> GetByUserIdAsync(int userId) => _repository.GetByUserIdAsync(userId);

    public Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId) => _repository.GetUnreadByUserIdAsync(userId);

    public Task<Notification?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public async Task<Notification> CreateNotificationAsync(CreateNotificationCommand command)
    {
        var notification = new Notification(
            command.UserId,
            command.Type,
            command.Title,
            command.Body,
            command.RelatedId,
            command.RelatedType
        );

        await _repository.AddAsync(notification);
        return notification;
    }

    public async Task<bool> MarkAsReadAsync(int id)
    {
        var notification = await _repository.GetByIdAsync(id);
        if (notification == null) return false;

        notification.MarkAsRead();
        await _repository.UpdateAsync(notification);
        return true;
    }

    public async Task<bool> MarkAllAsReadAsync(int userId)
    {
        var notifications = await _repository.GetUnreadByUserIdAsync(userId);
        foreach (var notification in notifications)
        {
            notification.MarkAsRead();
            await _repository.UpdateAsync(notification);
        }
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var notification = await _repository.GetByIdAsync(id);
        if (notification == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }

    public Task<int> GetUnreadCountAsync(int userId) => _repository.GetUnreadCountAsync(userId);
}
