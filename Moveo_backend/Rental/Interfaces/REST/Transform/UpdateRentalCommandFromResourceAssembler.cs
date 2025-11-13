using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Model.ValueObjects;
using Moveo_backend.Rental.Interfaces.REST.Resources;

namespace Moveo_backend.Rental.Interfaces.REST.Transform;

public static class UpdateRentalCommandFromResourceAssembler
{
    public static UpdateRentalCommand ToCommandFromResource(UpdateRentalResource resource)
    {
        return new UpdateRentalCommand(
            resource.Id,
            new Money(resource.TotalPrice)
        );
    }
}