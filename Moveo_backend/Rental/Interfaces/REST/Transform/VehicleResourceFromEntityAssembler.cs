using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Interfaces.REST.Resources;

namespace Moveo_backend.Rental.Interfaces.REST.Transform;

public static class VehicleResourceFromEntityAssembler
{
    public static VehicleResource ToResourceFromEntity(Vehicle vehicle)
    {
        return new VehicleResource(
            vehicle.Id,
            vehicle.OwnerId,
            vehicle.Brand,
            vehicle.Model,
            vehicle.Year,
            vehicle.Color,
            vehicle.Transmission,
            vehicle.FuelType,
            vehicle.Seats,
            vehicle.DailyPrice.Amount,
            vehicle.DepositAmount.Amount,
            vehicle.Location.Address,
            vehicle.Features.ToArray(),
            vehicle.Restrictions.ToArray(),
            vehicle.Photos.ToArray(),
            vehicle.Status
        );
    }
}