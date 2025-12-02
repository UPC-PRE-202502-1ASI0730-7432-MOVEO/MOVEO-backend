namespace Moveo_backend.Reviews.Interfaces.REST.Resources;

public record CreateReviewResource(
    string VehicleId,
    string RentalId,
    int ReviewerId,
    int OwnerId,
    int Rating,
    string? Comment
);
