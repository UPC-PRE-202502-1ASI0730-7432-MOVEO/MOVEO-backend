using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Model.ValueObjects;
using Moveo_backend.Rental.Interfaces.REST.Resources;

namespace Moveo_backend.Rental.Interfaces.REST.Transform;

public static class CreateRentalCommandFromResourceAssembler
{
    public static CreateRentalCommand ToCommand(CreateRentalResource resource)
    {
        return new CreateRentalCommand(
            resource.VehicleId,
            resource.RenterId,
            resource.OwnerId,
            new DateRange(resource.StartDate, resource.EndDate),
            new Money(resource.TotalPrice),
            new Location("", resource.PickupLocation, 0, 0),
            new Location("", resource.ReturnLocation, 0, 0),
            resource.Notes
        );
    }
}