namespace Moveo_backend.Rental.Domain.Model.Aggregates;

public class Review
{
    public int Id { get; private set; }
    public Guid RentalId { get; private set; }
    public Guid ReviewerId { get; private set; }
    public Guid RevieweeId { get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    public string Type { get; private set; } // "owner-to-renter", "renter-to-owner", "vehicle"
    public DateTime CreatedAt { get; private set; }
    
    // Navigation properties
    public Rental? Rental { get; private set; }
    
    protected Review() { }
    
    public Review(Guid rentalId, Guid reviewerId, Guid revieweeId, int rating, string comment, string type)
    {
        RentalId = rentalId;
        ReviewerId = reviewerId;
        RevieweeId = revieweeId;
        Rating = rating;
        Comment = comment ?? string.Empty;
        Type = type;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void Update(int? rating, string? comment)
    {
        if (rating.HasValue) Rating = rating.Value;
        if (comment != null) Comment = comment;
    }
}
