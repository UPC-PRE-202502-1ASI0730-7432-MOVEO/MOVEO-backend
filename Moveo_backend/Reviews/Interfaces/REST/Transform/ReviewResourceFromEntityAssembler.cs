using Moveo_backend.Reviews.Domain.Model.Aggregates;
using Moveo_backend.Reviews.Interfaces.REST.Resources;

namespace Moveo_backend.Reviews.Interfaces.REST.Transform;

public static class ReviewResourceFromEntityAssembler
{
    public static ReviewResource ToResourceFromEntity(Review review)
    {
        return new ReviewResource(
            review.Id.ToString(),
            review.VehicleId.ToString(),
            review.RentalId.ToString(),
            review.ReviewerId,
            review.OwnerId,
            review.Rating,
            review.Comment,
            review.CreatedAt,
            review.UpdatedAt
        );
    }
}
