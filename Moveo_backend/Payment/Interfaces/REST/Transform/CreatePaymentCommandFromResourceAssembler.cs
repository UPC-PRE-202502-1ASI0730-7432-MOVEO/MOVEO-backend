using Moveo_backend.Payment.Domain.Model.Commands;
using Moveo_backend.Payment.Interfaces.REST.Resources;

namespace Moveo_backend.Payment.Interfaces.REST.Transform;

public static class CreatePaymentCommandFromResourceAssembler
{
    public static CreatePaymentCommand ToCommandFromResource(CreatePaymentResource resource)
    {
        return new CreatePaymentCommand(
            resource.PayerId,
            resource.RecipientId,
            resource.RentalId,
            resource.Amount,
            resource.Currency,
            resource.Method,
            resource.Type,
            resource.Description,
            resource.DueDate
        );
    }
}
