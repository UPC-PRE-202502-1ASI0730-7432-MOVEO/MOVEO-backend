namespace Moveo_backend.Reviews.Interfaces.REST.Resources;

public record UpdateReviewResource(
    int Rating,
    string? Comment
);
