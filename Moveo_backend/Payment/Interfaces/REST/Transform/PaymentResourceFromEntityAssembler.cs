using Moveo_backend.Payment.Interfaces.REST.Resources;
using PaymentEntity = Moveo_backend.Payment.Domain.Model.Aggregate.Payment;

namespace Moveo_backend.Payment.Interfaces.REST.Transform;

public static class PaymentResourceFromEntityAssembler
{
    public static PaymentResource ToResourceFromEntity(PaymentEntity entity)
    {
        return new PaymentResource(
            entity.Id,
            entity.PayerId,
            entity.RecipientId,
            entity.RentalId,
            entity.Amount,
            entity.Currency,
            entity.Method,
            entity.Status,
            entity.TransactionId,
            entity.Type,
            entity.Description,
            entity.Reason,
            entity.DueDate,
            entity.CreatedAt,
            entity.CompletedAt
        );
    }
}
