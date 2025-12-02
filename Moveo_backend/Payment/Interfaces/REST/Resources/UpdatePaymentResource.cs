namespace Moveo_backend.Payment.Interfaces.REST.Resources;

public record UpdatePaymentResource(
    int PayerId,
    int RecipientId,
    int RentalId,
    decimal Amount,
    string Currency,
    string Method,
    string Status,
    string? TransactionId,
    string Type,
    string? Description,
    string? Reason,
    DateTime? DueDate,
    DateTime? CompletedAt
);

public record PatchPaymentResource(
    string? Status,
    string? TransactionId,
    string? Reason,
    DateTime? CompletedAt
);
