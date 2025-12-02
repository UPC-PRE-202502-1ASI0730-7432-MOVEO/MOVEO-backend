namespace Moveo_backend.Support.Interfaces.REST.Resources;

public record UpdateSupportTicketResource(
    string? Status,
    string? Priority,
    int? AssignedToId
);
