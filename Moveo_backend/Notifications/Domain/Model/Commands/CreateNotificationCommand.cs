namespace Moveo_backend.Notifications.Domain.Model.Commands;

public record CreateNotificationCommand(
    int UserId,
    string Type,
    string Title,
    string Body,
    Guid? RelatedId = null,
    string? RelatedType = null
);
