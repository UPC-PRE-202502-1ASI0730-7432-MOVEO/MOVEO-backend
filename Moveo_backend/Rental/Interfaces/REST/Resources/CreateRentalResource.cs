namespace Moveo_backend.Rental.Interfaces.REST.Resources;

public record CreateRentalResource(
    Guid VehicleId,
    int RenterId,
    int OwnerId,
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalPrice,
    string PickupLocation,
    string ReturnLocation,
    string? Notes
);