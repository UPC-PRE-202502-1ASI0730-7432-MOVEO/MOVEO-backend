using Microsoft.EntityFrameworkCore;
using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Repositories;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Moveo_backend.Rental.Infrastructure.Persistence.EFC.Repository;

public class VehicleRepository : IVehicleRepository
{
    private readonly AppDbContext _context;

    public VehicleRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Vehicle>> GetAllAsync() =>
        _context.Vehicles.AsNoTracking().ToListAsync().ContinueWith(t => t.Result.AsEnumerable());

    public Task<IEnumerable<Vehicle>> GetByOwnerIdAsync(Guid ownerId) =>
        _context.Vehicles.Where(v => v.OwnerId == ownerId).AsNoTracking().ToListAsync()
            .ContinueWith(t => t.Result.AsEnumerable());

    public Task<IEnumerable<Vehicle>> GetAvailableAsync(
        string? location = null,
        string? brand = null,
        string? fuelType = null,
        decimal? minPrice = null,
        decimal? maxPrice = null)
    {
        var query = _context.Vehicles.AsQueryable();

        if (!string.IsNullOrEmpty(location)) query = query.Where(v => v.Location.Address.Contains(location));
        if (!string.IsNullOrEmpty(brand)) query = query.Where(v => v.Brand == brand);
        if (!string.IsNullOrEmpty(fuelType)) query = query.Where(v => v.FuelType == fuelType);
        if (minPrice.HasValue) query = query.Where(v => v.DailyPrice.Amount >= minPrice.Value);
        if (maxPrice.HasValue) query = query.Where(v => v.DailyPrice.Amount <= maxPrice.Value);

        return query.AsNoTracking().ToListAsync().ContinueWith(t => t.Result.AsEnumerable());
    }

    public Task<Vehicle?> GetByIdAsync(Guid id) =>
        _context.Vehicles.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

    public async Task AddAsync(Vehicle vehicle)
    {
        await _context.Vehicles.AddAsync(vehicle);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle != null)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
