namespace Moveo_backend.Rental.Interfaces.REST.Resources;

public record CreateRentalResource(
    Guid VehicleId,
    Guid RenterId,
    Guid OwnerId,
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalPrice,
    string PickupLocation,
    string ReturnLocation,
    string? Notes
);