namespace Moveo_backend.Rental.Interfaces.REST.Resources;

public record CreateReviewResource(
    Guid RentalId,
    Guid ReviewerId,
    Guid RevieweeId,
    int Rating,
    string? Comment,
    string Type);
