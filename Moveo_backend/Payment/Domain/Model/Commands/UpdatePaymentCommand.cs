namespace Moveo_backend.Payment.Domain.Model.Commands;

public record UpdatePaymentCommand(
    int Id,
    string? Status = null,
    string? TransactionId = null,
    DateTime? CompletedAt = null
);
