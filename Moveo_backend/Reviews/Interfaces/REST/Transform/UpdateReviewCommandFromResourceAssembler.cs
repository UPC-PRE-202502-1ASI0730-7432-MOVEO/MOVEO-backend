using Moveo_backend.Reviews.Domain.Model.Commands;
using Moveo_backend.Reviews.Interfaces.REST.Resources;

namespace Moveo_backend.Reviews.Interfaces.REST.Transform;

public static class UpdateReviewCommandFromResourceAssembler
{
    public static UpdateReviewCommand ToCommand(UpdateReviewResource resource)
    {
        return new UpdateReviewCommand(
            resource.Rating,
            resource.Comment
        );
    }
}
