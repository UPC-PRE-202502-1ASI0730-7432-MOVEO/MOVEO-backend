namespace Moveo_backend.Notification.Interfaces.REST.Resources;

public record NotificationResource(
    int Id,
    int UserId,
    string Title,
    string Body,
    string Type,
    bool IsRead,
    int? RelatedEntityId,
    string? RelatedEntityType,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
