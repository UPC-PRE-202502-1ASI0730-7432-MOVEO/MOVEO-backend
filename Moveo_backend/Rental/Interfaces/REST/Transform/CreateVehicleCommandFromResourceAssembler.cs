using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Model.ValueObjects;
using Moveo_backend.Rental.Interfaces.REST.Resources;

namespace Moveo_backend.Rental.Interfaces.REST.Transform;

public static class CreateVehicleCommandFromResourceAssembler
{
    public static CreateVehicleCommand ToCommand(CreateVehicleResource resource)
    {
        return new CreateVehicleCommand(
            resource.OwnerId,
            resource.Brand,
            resource.Model,
            resource.Year,
            resource.Color,
            resource.Transmission,
            resource.FuelType,
            resource.Seats,
            new Money(resource.DailyPrice),       // decimal -> Money
            new Money(resource.DepositAmount),    // decimal -> Money
            new Location(resource.Location),      // string -> Location
            resource.Features.ToList(),           // string[] -> List<string>
            resource.Restrictions.ToList(),
            resource.Photos.ToList()
        );
    }
}