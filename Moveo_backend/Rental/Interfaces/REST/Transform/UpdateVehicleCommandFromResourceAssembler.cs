using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Model.ValueObjects;
using Moveo_backend.Rental.Interfaces.REST.Resources;

namespace Moveo_backend.Rental.Interfaces.REST.Transform;

public static class UpdateVehicleCommandFromResourceAssembler
{
    public static UpdateVehicleCommand ToCommandFromResource(UpdateVehicleResource resource)
    {
        return new UpdateVehicleCommand(
            resource.Id,
            resource.Brand,
            resource.Model,
            resource.Year,
            resource.Color,
            resource.Transmission,
            resource.FuelType,
            resource.Seats,
            new Money(resource.DailyPrice),
            new Money(resource.DepositAmount),
            new Location(resource.Location),
            resource.Features.ToList(),
            resource.Restrictions.ToList(),
            resource.Photos.ToList()
        );
    }
}