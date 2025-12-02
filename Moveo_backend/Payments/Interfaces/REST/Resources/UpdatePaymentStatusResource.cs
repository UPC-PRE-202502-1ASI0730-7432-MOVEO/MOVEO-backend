namespace Moveo_backend.Payments.Interfaces.REST.Resources;

public record UpdatePaymentStatusResource(
    string Status,
    string? TransactionId,
    string? FailureReason
);
