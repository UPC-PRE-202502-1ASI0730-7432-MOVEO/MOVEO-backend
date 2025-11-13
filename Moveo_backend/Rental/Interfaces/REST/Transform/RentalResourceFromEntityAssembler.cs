using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Interfaces.REST.Resources;

namespace Moveo_backend.Rental.Interfaces.REST.Transform;

public static class RentalResourceFromEntityAssembler
{
    public static RentalResource ToResourceFromEntity(Domain.Model.Aggregates.Rental rental)
    {
        return new RentalResource(
            rental.Id,
            rental.VehicleId,
            rental.RenterId,
            rental.OwnerId,
            rental.Status,
            rental.RentalPeriod.StartDate,
            rental.RentalPeriod.EndDate,
            rental.TotalPrice.Amount,
            rental.PickupLocation.Address,
            rental.ReturnLocation.Address,
            rental.Notes
        );
    }
}