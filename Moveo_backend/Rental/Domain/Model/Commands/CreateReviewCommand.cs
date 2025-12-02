namespace Moveo_backend.Rental.Domain.Model.Commands;

public record CreateReviewCommand(
    Guid RentalId,
    Guid ReviewerId,
    Guid RevieweeId,
    int Rating,
    string? Comment,
    string Type);
