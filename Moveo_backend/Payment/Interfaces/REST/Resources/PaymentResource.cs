namespace Moveo_backend.Payment.Interfaces.REST.Resources;

public record PaymentResource(
    int Id,
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
    DateTime CreatedAt,
    DateTime UpdatedAt
);
