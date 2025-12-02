namespace Moveo_backend.SupportTickets.Domain.Model.Commands;

public record CreateSupportTicketCommand(
    int UserId,
    string Subject,
    string Description,
    string Category,
    string Priority,
    Guid? RelatedRentalId = null,
    Guid? RelatedVehicleId = null
);

public record UpdateSupportTicketCommand(
    int Id,
    string Subject,
    string Description,
    string Category,
    string Priority
);

public record AssignTicketCommand(int TicketId, int AdminId);
public record ResolveTicketCommand(int TicketId, string Resolution);
public record CloseTicketCommand(int TicketId);
public record ReopenTicketCommand(int TicketId);
