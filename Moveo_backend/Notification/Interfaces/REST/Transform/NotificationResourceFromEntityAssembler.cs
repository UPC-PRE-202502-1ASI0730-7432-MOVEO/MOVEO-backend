using Moveo_backend.Notification.Interfaces.REST.Resources;
using NotificationEntity = Moveo_backend.Notification.Domain.Model.Aggregate.Notification;

namespace Moveo_backend.Notification.Interfaces.REST.Transform;

public static class NotificationResourceFromEntityAssembler
{
    public static NotificationResource ToResourceFromEntity(NotificationEntity entity)
    {
        return new NotificationResource(
            entity.Id,
            entity.UserId,
            entity.Title,
            entity.Body,
            entity.Type,
            entity.IsRead,
            entity.RelatedEntityId,
            entity.RelatedEntityType,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}
