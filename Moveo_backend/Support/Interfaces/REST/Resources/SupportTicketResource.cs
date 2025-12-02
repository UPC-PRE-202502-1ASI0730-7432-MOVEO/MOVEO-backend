namespace Moveo_backend.Support.Interfaces.REST.Resources;

public record SupportTicketResource(
    int Id,
    int UserId,
    string Subject,
    string Description,
    string Category,
    string Status,
    string Priority,
    int? AssignedToId,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime? ResolvedAt,
    IEnumerable<TicketMessageResource>? Messages
);
