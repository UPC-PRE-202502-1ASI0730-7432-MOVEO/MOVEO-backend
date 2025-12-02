namespace Moveo_backend.Support.Interfaces.REST.Resources;

public record CreateSupportTicketResource(
    int UserId,
    string Subject,
    string Description,
    string Category,
    string? Priority
);
