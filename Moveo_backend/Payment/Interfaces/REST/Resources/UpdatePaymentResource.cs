namespace Moveo_backend.Payment.Interfaces.REST.Resources;

public record UpdatePaymentResource(
    decimal? Amount,
    string? Currency,
    string? Method,
    string? Status,
    string? TransactionId,
    string? Type,
    string? Description,
    string? Reason,
    DateTime? DueDate
);
