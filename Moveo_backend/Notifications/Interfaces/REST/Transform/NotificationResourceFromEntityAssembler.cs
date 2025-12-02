using Moveo_backend.Notifications.Domain.Model.Aggregates;
using Moveo_backend.Notifications.Interfaces.REST.Resources;

namespace Moveo_backend.Notifications.Interfaces.REST.Transform;

public static class NotificationResourceFromEntityAssembler
{
    public static NotificationResource ToResourceFromEntity(Notification notification)
    {
        return new NotificationResource(
            notification.Id,
            notification.UserId,
            notification.Type,
            notification.Title,
            notification.Body,
            notification.Read,
            notification.CreatedAt,
            notification.RelatedId,
            notification.RelatedType
        );
    }
}
