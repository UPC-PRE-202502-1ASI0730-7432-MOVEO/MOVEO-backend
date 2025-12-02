using Microsoft.EntityFrameworkCore;
using Moveo_backend.Payments.Domain.Model.Aggregates;
using Moveo_backend.Payments.Domain.Repositories;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Moveo_backend.Payments.Infrastructure.Persistence;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Payment?> GetByIdAsync(Guid id)
    {
        return await _context.Payments.FindAsync(id);
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _context.Payments.ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetByPayerIdAsync(int payerId)
    {
        return await _context.Payments
            .Where(p => p.PayerId == payerId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetByRecipientIdAsync(int recipientId)
    {
        return await _context.Payments
            .Where(p => p.RecipientId == recipientId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetByRentalIdAsync(Guid rentalId)
    {
        return await _context.Payments
            .Where(p => p.RentalId == rentalId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetByStatusAsync(string status)
    {
        return await _context.Payments
            .Where(p => p.Status == status)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Payment payment)
    {
        _context.Payments.Update(payment);
        await _context.SaveChangesAsync();
    }
}
