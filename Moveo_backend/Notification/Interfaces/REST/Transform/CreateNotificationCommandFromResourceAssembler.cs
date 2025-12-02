using Moveo_backend.Notification.Domain.Model.Commands;
using Moveo_backend.Notification.Interfaces.REST.Resources;

namespace Moveo_backend.Notification.Interfaces.REST.Transform;

public static class CreateNotificationCommandFromResourceAssembler
{
    public static CreateNotificationCommand ToCommandFromResource(CreateNotificationResource resource)
    {
        return new CreateNotificationCommand(
            resource.UserId,
            resource.Title,
            resource.Body,
            resource.Type,
            resource.RelatedEntityId,
            resource.RelatedEntityType
        );
    }
}
