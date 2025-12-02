using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Shared.Domain.Repositories;

namespace Moveo_backend.Rental.Domain.Repositories;

public interface IReviewRepository : IBaseRepository<Review>
{
    Task<IEnumerable<Review>> FindByRentalIdAsync(Guid rentalId);
    Task<IEnumerable<Review>> FindByReviewerIdAsync(Guid reviewerId);
    Task<IEnumerable<Review>> FindByRevieweeIdAsync(Guid revieweeId);
}
