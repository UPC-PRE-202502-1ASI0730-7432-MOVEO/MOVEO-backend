namespace Moveo_backend.Payments.Interfaces.REST.Resources;

public record CreatePaymentResource(
    string RentalId,
    int PayerId,
    int RecipientId,
    decimal Amount,
    string Currency,
    string? PaymentMethod = "card",
    string? Description = null
);
