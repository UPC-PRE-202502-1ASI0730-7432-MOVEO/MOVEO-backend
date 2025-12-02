using Moveo_backend.Notifications.Domain.Model.Aggregates;
using Moveo_backend.Notifications.Domain.Repositories;

namespace Moveo_backend.Notifications.Application.QueryServices;

public class NotificationQueryService
{
    private readonly INotificationRepository _repository;

    public NotificationQueryService(INotificationRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Notification>> GetAllAsync() => _repository.GetAllAsync();

    public Task<IEnumerable<Notification>> GetByUserIdAsync(int userId) => _repository.GetByUserIdAsync(userId);

    public Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId) => _repository.GetUnreadByUserIdAsync(userId);

    public Task<Notification?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public Task<int> GetUnreadCountAsync(int userId) => _repository.GetUnreadCountAsync(userId);
}
