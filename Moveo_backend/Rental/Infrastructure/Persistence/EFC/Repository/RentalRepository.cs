using Microsoft.EntityFrameworkCore;
using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Repositories;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Moveo_backend.Rental.Infrastructure.Persistence.EFC.Repository;

public class RentalRepository : IRentalRepository
{
    private readonly AppDbContext _context;

    public RentalRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Domain.Model.Aggregates.Rental>> GetAllAsync() =>
        _context.Rentals.AsNoTracking().ToListAsync().ContinueWith(t => t.Result.AsEnumerable());

    public Task<IEnumerable<Domain.Model.Aggregates.Rental>> GetActiveAsync() =>
        _context.Rentals.Where(r => r.Status == "Active").AsNoTracking().ToListAsync()
            .ContinueWith(t => t.Result.AsEnumerable());

    public Task<Domain.Model.Aggregates.Rental?> GetByIdAsync(Guid id) =>
        _context.Rentals.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

    public Task<IEnumerable<Domain.Model.Aggregates.Rental>> GetByUserIdAsync(Guid userId) =>
        _context.Rentals.Where(r => r.RenterId == userId || r.OwnerId == userId)
            .AsNoTracking().ToListAsync()
            .ContinueWith(t => t.Result.AsEnumerable());

    public async Task AddAsync(Domain.Model.Aggregates.Rental rental)
    {
        await _context.Rentals.AddAsync(rental);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Model.Aggregates.Rental rental)
    {
        _context.Rentals.Update(rental);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var rental = await _context.Rentals.FindAsync(id);
        if (rental != null)
        {
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
        }
    }

    public Task<bool> IsVehicleCurrentlyRentedAsync(Guid vehicleId) =>
        _context.Rentals.AnyAsync(r => r.VehicleId == vehicleId && r.Status == "Active");
}