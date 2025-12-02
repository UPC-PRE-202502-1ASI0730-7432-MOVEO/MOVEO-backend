using Moveo_backend.Rental.Domain.Model.ValueObjects;

namespace Moveo_backend.Rental.Domain.Model.Commands;

public record CreateRentalCommand(
    Guid VehicleId,
    int RenterId,
    int OwnerId,
    DateRange RentalPeriod,
    Money TotalPrice,
    Location PickupLocation,
    Location ReturnLocation,
    string? Notes);
