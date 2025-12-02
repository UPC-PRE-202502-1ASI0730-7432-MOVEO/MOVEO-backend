namespace Moveo_backend.Payments.Interfaces.REST.Resources;

public record PaymentResource(
    string Id,
    string RentalId,
    int PayerId,
    int RecipientId,
    decimal Amount,
    string Currency,
    string Status,
    string PaymentMethod,
    string? TransactionId,
    string? Description,
    DateTime CreatedAt,
    DateTime? PaidAt,
    DateTime? FailedAt,
    string? FailureReason
);
