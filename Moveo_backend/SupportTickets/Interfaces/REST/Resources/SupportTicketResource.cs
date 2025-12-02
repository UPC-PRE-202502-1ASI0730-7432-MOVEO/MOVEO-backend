namespace Moveo_backend.SupportTickets.Interfaces.REST.Resources;

public record SupportTicketResource(
    int Id,
    int UserId,
    string Subject,
    string Description,
    string Category,
    string Priority,
    string Status,
    Guid? RelatedRentalId,
    Guid? RelatedVehicleId,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? ResolvedAt,
    string? Resolution,
    int? AssignedToId
);

public record CreateSupportTicketResource(
    int UserId,
    string Subject,
    string Description,
    string Category,
    string Priority,
    Guid? RelatedRentalId = null,
    Guid? RelatedVehicleId = null
);

public record UpdateSupportTicketResource(
    string Subject,
    string Description,
    string Category,
    string Priority
);

public record ResolveTicketResource(string Resolution);
public record AssignTicketResource(int AdminId);
