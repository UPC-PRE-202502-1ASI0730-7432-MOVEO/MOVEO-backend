namespace Moveo_backend.Reviews.Domain.Model.Commands;

public record UpdateReviewCommand(
    int Rating,
    string? Comment
);
