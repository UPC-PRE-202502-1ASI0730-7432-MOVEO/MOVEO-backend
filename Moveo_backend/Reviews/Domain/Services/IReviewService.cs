using Moveo_backend.Reviews.Domain.Model.Aggregates;
using Moveo_backend.Reviews.Domain.Model.Commands;

namespace Moveo_backend.Reviews.Domain.Services;

public interface IReviewService
{
    Task<Review?> GetByIdAsync(Guid id);
    Task<IEnumerable<Review>> GetAllAsync();
    Task<IEnumerable<Review>> GetByVehicleIdAsync(Guid vehicleId);
    Task<IEnumerable<Review>> GetByReviewerIdAsync(int reviewerId);
    Task<IEnumerable<Review>> GetByOwnerIdAsync(int ownerId);
    Task<Review?> GetByRentalIdAsync(Guid rentalId);
    Task<Review> CreateReviewAsync(CreateReviewCommand command);
    Task<Review> UpdateReviewAsync(Guid id, UpdateReviewCommand command);
    Task DeleteReviewAsync(Guid id);
    Task<double> GetAverageRatingForVehicleAsync(Guid vehicleId);
    Task<double> GetAverageRatingForOwnerAsync(int ownerId);
}
