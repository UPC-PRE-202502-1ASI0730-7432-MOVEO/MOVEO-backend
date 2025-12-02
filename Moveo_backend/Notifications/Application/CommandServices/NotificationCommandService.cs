using Moveo_backend.Notifications.Domain.Model.Aggregates;
using Moveo_backend.Notifications.Domain.Model.Commands;
using Moveo_backend.Notifications.Domain.Repositories;

namespace Moveo_backend.Notifications.Application.CommandServices;

public class NotificationCommandService
{
    private readonly INotificationRepository _repository;

    public NotificationCommandService(INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Notification> Handle(CreateNotificationCommand command)
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

    public async Task<bool> HandleMarkAsRead(int id)
    {
        var notification = await _repository.GetByIdAsync(id);
        if (notification == null) return false;

        notification.MarkAsRead();
        await _repository.UpdateAsync(notification);
        return true;
    }

    public async Task<bool> HandleMarkAllAsRead(int userId)
    {
        var notifications = await _repository.GetUnreadByUserIdAsync(userId);
        foreach (var notification in notifications)
        {
            notification.MarkAsRead();
            await _repository.UpdateAsync(notification);
        }
        return true;
    }

    public async Task<bool> HandleDelete(int id)
    {
        var notification = await _repository.GetByIdAsync(id);
        if (notification == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}
