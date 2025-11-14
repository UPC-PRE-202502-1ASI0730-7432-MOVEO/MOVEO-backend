namespace Moveo_backend.Rental.Interfaces.REST.Resources;

public record RentalResource(
    Guid Id,
    Guid VehicleId,
    Guid RenterId,
    Guid OwnerId,
    string Status,
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalPrice,
    string PickupLocation,
    string ReturnLocation,
    string? Notes
);