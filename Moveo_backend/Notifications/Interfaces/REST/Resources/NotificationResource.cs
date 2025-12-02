namespace Moveo_backend.Notifications.Interfaces.REST.Resources;

public record NotificationResource(
    int Id,
    int UserId,
    string Type,
    string Title,
    string Body,
    bool Read,
    DateTime CreatedAt,
    Guid? RelatedId,
    string? RelatedType
);

public record CreateNotificationResource(
    int UserId,
    string Type,
    string Title,
    string Body,
    Guid? RelatedId = null,
    string? RelatedType = null
);

public record MarkAsReadResource(bool Read);
