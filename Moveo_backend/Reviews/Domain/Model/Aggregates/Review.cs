namespace Moveo_backend.Reviews.Domain.Model.Aggregates;

public class Review
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public Guid RentalId { get; private set; }
    public int ReviewerId { get; private set; }
    public int OwnerId { get; private set; }
    public int Rating { get; private set; }
    public string? Comment { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Review() { }

    public Review(
        Guid vehicleId,
        Guid rentalId,
        int reviewerId,
        int ownerId,
        int rating,
        string? comment = null)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5");

        Id = Guid.NewGuid();
        VehicleId = vehicleId;
        RentalId = rentalId;
        ReviewerId = reviewerId;
        OwnerId = ownerId;
        Rating = rating;
        Comment = comment;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateReview(int rating, string? comment)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5");

        Rating = rating;
        Comment = comment;
        UpdatedAt = DateTime.UtcNow;
    }
}
