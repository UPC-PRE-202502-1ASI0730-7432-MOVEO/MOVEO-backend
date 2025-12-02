using Moveo_backend.Reviews.Domain.Model.Commands;
using Moveo_backend.Reviews.Interfaces.REST.Resources;

namespace Moveo_backend.Reviews.Interfaces.REST.Transform;

public static class CreateReviewCommandFromResourceAssembler
{
    public static CreateReviewCommand ToCommand(CreateReviewResource resource)
    {
        return new CreateReviewCommand(
            Guid.Parse(resource.VehicleId),
            Guid.Parse(resource.RentalId),
            resource.ReviewerId,
            resource.OwnerId,
            resource.Rating,
            resource.Comment
        );
    }
}
