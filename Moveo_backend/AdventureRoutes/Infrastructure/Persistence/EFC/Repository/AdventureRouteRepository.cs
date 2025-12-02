using Microsoft.EntityFrameworkCore;
using Moveo_backend.AdventureRoutes.Domain.Model.Aggregates;
using Moveo_backend.AdventureRoutes.Domain.Repositories;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Moveo_backend.AdventureRoutes.Infrastructure.Persistence.EFC.Repository;

public class AdventureRouteRepository : IAdventureRouteRepository
{
    private readonly AppDbContext _context;

    public AdventureRouteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AdventureRoute>> GetAllAsync() =>
        await _context.AdventureRoutes.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<AdventureRoute>> GetActiveAsync() =>
        await _context.AdventureRoutes.Where(r => r.IsActive).AsNoTracking().ToListAsync();

    public async Task<AdventureRoute?> GetByIdAsync(int id) =>
        await _context.AdventureRoutes.FirstOrDefaultAsync(r => r.Id == id);

    public async Task AddAsync(AdventureRoute route)
    {
        await _context.AdventureRoutes.AddAsync(route);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AdventureRoute route)
    {
        _context.AdventureRoutes.Update(route);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var route = await _context.AdventureRoutes.FindAsync(id);
        if (route != null)
        {
            _context.AdventureRoutes.Remove(route);
            await _context.SaveChangesAsync();
        }
    }
}
