namespace Moveo_backend.Payment.Interfaces.REST.Resources;

public record UpdatePaymentResource(
    string? Status,
    string? TransactionId
);
