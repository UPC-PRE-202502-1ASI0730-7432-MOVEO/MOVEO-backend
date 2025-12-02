namespace Moveo_backend.Support.Domain.Model.Commands;

public record CreateSupportTicketCommand(
    int UserId,
    string Subject,
    string Description,
    string Category,
    string? Priority
);
