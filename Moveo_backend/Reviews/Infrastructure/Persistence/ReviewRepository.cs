using Microsoft.EntityFrameworkCore;
using Moveo_backend.Reviews.Domain.Model.Aggregates;
using Moveo_backend.Reviews.Domain.Repositories;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Moveo_backend.Reviews.Infrastructure.Persistence;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Review?> GetByIdAsync(Guid id)
    {
        return await _context.Reviews.FindAsync(id);
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Reviews
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByVehicleIdAsync(Guid vehicleId)
    {
        return await _context.Reviews
            .Where(r => r.VehicleId == vehicleId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByReviewerIdAsync(int reviewerId)
    {
        return await _context.Reviews
            .Where(r => r.ReviewerId == reviewerId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByOwnerIdAsync(int ownerId)
    {
        return await _context.Reviews
            .Where(r => r.OwnerId == ownerId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<Review?> GetByRentalIdAsync(Guid rentalId)
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(r => r.RentalId == rentalId);
    }

    public async Task AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Review review)
    {
        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
