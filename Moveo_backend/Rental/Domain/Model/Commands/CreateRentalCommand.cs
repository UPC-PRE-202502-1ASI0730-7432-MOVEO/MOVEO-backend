using Moveo_backend.Rental.Domain.Model.ValueObjects;

namespace Moveo_backend.Rental.Domain.Model.Commands;

public record CreateRentalCommand(
    Guid VehicleId,
    Guid RenterId,
    Guid OwnerId,
    DateRange RentalPeriod,
    Money TotalPrice,
    Location PickupLocation,
    Location ReturnLocation,
    string? Notes);
