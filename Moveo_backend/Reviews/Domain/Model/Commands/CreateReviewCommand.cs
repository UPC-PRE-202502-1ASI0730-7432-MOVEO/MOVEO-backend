namespace Moveo_backend.Reviews.Domain.Model.Commands;

public record CreateReviewCommand(
    Guid VehicleId,
    Guid RentalId,
    int ReviewerId,
    int OwnerId,
    int Rating,
    string? Comment
);
