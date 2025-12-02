namespace Moveo_backend.Notification.Interfaces.REST.Resources;

public record CreateNotificationResource(
    int UserId,
    string Title,
    string Body,
    string Type,
    int? RelatedEntityId,
    string? RelatedEntityType
);
