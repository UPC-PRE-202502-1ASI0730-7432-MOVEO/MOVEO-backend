using Moveo_backend.Payments.Domain.Model.Commands;
using Moveo_backend.Payments.Interfaces.REST.Resources;

namespace Moveo_backend.Payments.Interfaces.REST.Transform;

public static class CreatePaymentCommandFromResourceAssembler
{
    public static CreatePaymentCommand ToCommand(CreatePaymentResource resource)
    {
        return new CreatePaymentCommand(
            Guid.Parse(resource.RentalId),
            resource.PayerId,
            resource.RecipientId,
            resource.Amount,
            resource.Currency,
            resource.PaymentMethod ?? "card",
            resource.Description
        );
    }
}
