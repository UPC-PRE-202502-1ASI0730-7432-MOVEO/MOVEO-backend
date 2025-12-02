using Moveo_backend.Reviews.Domain.Model.Aggregates;

namespace Moveo_backend.Reviews.Domain.Repositories;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(Guid id);
    Task<IEnumerable<Review>> GetAllAsync();
    Task<IEnumerable<Review>> GetByVehicleIdAsync(Guid vehicleId);
    Task<IEnumerable<Review>> GetByReviewerIdAsync(int reviewerId);
    Task<IEnumerable<Review>> GetByOwnerIdAsync(int ownerId);
    Task<Review?> GetByRentalIdAsync(Guid rentalId);
    Task AddAsync(Review review);
    Task UpdateAsync(Review review);
    Task DeleteAsync(Guid id);
}
