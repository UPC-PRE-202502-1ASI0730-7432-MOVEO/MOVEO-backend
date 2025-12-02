using Moveo_backend.Reviews.Domain.Model.Aggregates;
using Moveo_backend.Reviews.Domain.Model.Commands;
using Moveo_backend.Reviews.Domain.Repositories;
using Moveo_backend.Reviews.Domain.Services;

namespace Moveo_backend.Reviews.Application;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<Review?> GetByIdAsync(Guid id)
    {
        return await _reviewRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _reviewRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Review>> GetByVehicleIdAsync(Guid vehicleId)
    {
        return await _reviewRepository.GetByVehicleIdAsync(vehicleId);
    }

    public async Task<IEnumerable<Review>> GetByReviewerIdAsync(int reviewerId)
    {
        return await _reviewRepository.GetByReviewerIdAsync(reviewerId);
    }

    public async Task<IEnumerable<Review>> GetByOwnerIdAsync(int ownerId)
    {
        return await _reviewRepository.GetByOwnerIdAsync(ownerId);
    }

    public async Task<Review?> GetByRentalIdAsync(Guid rentalId)
    {
        return await _reviewRepository.GetByRentalIdAsync(rentalId);
    }

    public async Task<Review> CreateReviewAsync(CreateReviewCommand command)
    {
        // Check if a review already exists for this rental
        var existingReview = await _reviewRepository.GetByRentalIdAsync(command.RentalId);
        if (existingReview != null)
            throw new InvalidOperationException("A review already exists for this rental");

        var review = new Review(
            command.VehicleId,
            command.RentalId,
            command.ReviewerId,
            command.OwnerId,
            command.Rating,
            command.Comment
        );

        await _reviewRepository.AddAsync(review);
        return review;
    }

    public async Task<Review> UpdateReviewAsync(Guid id, UpdateReviewCommand command)
    {
        var review = await _reviewRepository.GetByIdAsync(id);
        if (review == null)
            throw new KeyNotFoundException($"Review with id {id} not found");

        review.UpdateReview(command.Rating, command.Comment);
        await _reviewRepository.UpdateAsync(review);
        return review;
    }

    public async Task DeleteReviewAsync(Guid id)
    {
        var review = await _reviewRepository.GetByIdAsync(id);
        if (review == null)
            throw new KeyNotFoundException($"Review with id {id} not found");

        await _reviewRepository.DeleteAsync(id);
    }

    public async Task<double> GetAverageRatingForVehicleAsync(Guid vehicleId)
    {
        var reviews = await _reviewRepository.GetByVehicleIdAsync(vehicleId);
        var reviewsList = reviews.ToList();
        if (!reviewsList.Any())
            return 0;

        return reviewsList.Average(r => r.Rating);
    }

    public async Task<double> GetAverageRatingForOwnerAsync(int ownerId)
    {
        var reviews = await _reviewRepository.GetByOwnerIdAsync(ownerId);
        var reviewsList = reviews.ToList();
        if (!reviewsList.Any())
            return 0;

        return reviewsList.Average(r => r.Rating);
    }
}
