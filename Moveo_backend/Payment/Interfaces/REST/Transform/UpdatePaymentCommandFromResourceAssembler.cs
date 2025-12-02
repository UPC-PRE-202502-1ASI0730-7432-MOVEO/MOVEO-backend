using Moveo_backend.Payment.Domain.Model.Commands;
using Moveo_backend.Payment.Interfaces.REST.Resources;

namespace Moveo_backend.Payment.Interfaces.REST.Transform;

public static class UpdatePaymentCommandFromResourceAssembler
{
    public static UpdatePaymentCommand ToCommandFromResource(int id, UpdatePaymentResource resource)
    {
        return new UpdatePaymentCommand(
            id,
            resource.Status,
            resource.TransactionId
        );
    }
}
