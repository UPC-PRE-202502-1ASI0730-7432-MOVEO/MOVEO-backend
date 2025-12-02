namespace Moveo_backend.Reviews.Interfaces.REST.Resources;

public record ReviewResource(
    string Id,
    string VehicleId,
    string RentalId,
    int ReviewerId,
    int OwnerId,
    int Rating,
    string? Comment,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
