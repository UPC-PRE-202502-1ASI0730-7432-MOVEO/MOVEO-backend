namespace Moveo_backend.Payments.Domain.Model.Commands;

public record CreatePaymentCommand(
    Guid RentalId,
    int PayerId,
    int RecipientId,
    decimal Amount,
    string Currency,
    string PaymentMethod,
    string? Description
);
