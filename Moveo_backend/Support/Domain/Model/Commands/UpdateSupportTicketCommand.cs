namespace Moveo_backend.Support.Domain.Model.Commands;

public record UpdateSupportTicketCommand(
    int Id,
    string? Status,
    string? Priority,
    int? AssignedToId
);
