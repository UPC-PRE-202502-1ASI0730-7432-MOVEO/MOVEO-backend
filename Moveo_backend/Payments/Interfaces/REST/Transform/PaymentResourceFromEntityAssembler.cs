using Moveo_backend.Payments.Domain.Model.Aggregates;
using Moveo_backend.Payments.Interfaces.REST.Resources;

namespace Moveo_backend.Payments.Interfaces.REST.Transform;

public static class PaymentResourceFromEntityAssembler
{
    public static PaymentResource ToResourceFromEntity(Payment payment)
    {
        return new PaymentResource(
            payment.Id.ToString(),
            payment.RentalId.ToString(),
            payment.PayerId,
            payment.RecipientId,
            payment.Amount,
            payment.Currency,
            payment.Status,
            payment.PaymentMethod,
            payment.TransactionId,
            payment.Description,
            payment.CreatedAt,
            payment.PaidAt,
            payment.FailedAt,
            payment.FailureReason
        );
    }
}
