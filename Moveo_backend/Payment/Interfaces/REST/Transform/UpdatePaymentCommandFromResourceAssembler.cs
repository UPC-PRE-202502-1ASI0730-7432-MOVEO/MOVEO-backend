using Moveo_backend.Payment.Domain.Model.Commands;
using Moveo_backend.Payment.Interfaces.REST.Resources;

namespace Moveo_backend.Payment.Interfaces.REST.Transform;

public static class UpdatePaymentCommandFromResourceAssembler
{
    public static UpdatePaymentCommand ToCommandFromResource(int id, UpdatePaymentResource resource)
    {
        return new UpdatePaymentCommand(
            id,
            resource.PayerId,
            resource.RecipientId,
            resource.RentalId,
            resource.Amount,
            resource.Currency,
            resource.Method,
            resource.Status,
            resource.TransactionId,
            resource.Type,
            resource.Description,
            resource.Reason,
            resource.DueDate,
            resource.CompletedAt
        );
    }

    public static PatchPaymentCommand ToPatchCommandFromResource(int id, PatchPaymentResource resource)
    {
        return new PatchPaymentCommand(
            id,
            resource.Status,
            resource.TransactionId,
            resource.Reason,
            resource.CompletedAt
        );
    }
}
