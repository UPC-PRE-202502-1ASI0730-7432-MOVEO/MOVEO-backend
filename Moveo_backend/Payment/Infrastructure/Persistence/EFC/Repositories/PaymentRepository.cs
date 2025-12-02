using Microsoft.EntityFrameworkCore;
using Moveo_backend.Payment.Domain.Repositories;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using PaymentEntity = Moveo_backend.Payment.Domain.Model.Aggregate.Payment;

namespace Moveo_backend.Payment.Infrastructure.Persistence.EFC.Repositories;

public class PaymentRepository(AppDbContext context) 
    : BaseRepository<PaymentEntity>(context), IPaymentRepository
{
    public async Task<IEnumerable<PaymentEntity>> FindByPayerIdAsync(int payerId)
    {
        return await Context.Set<PaymentEntity>()
            .Where(p => p.PayerId == payerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<PaymentEntity>> FindByRecipientIdAsync(int recipientId)
    {
        return await Context.Set<PaymentEntity>()
            .Where(p => p.RecipientId == recipientId)
            .ToListAsync();
    }

    public async Task<IEnumerable<PaymentEntity>> FindByRentalIdAsync(int rentalId)
    {
        return await Context.Set<PaymentEntity>()
            .Where(p => p.RentalId == rentalId)
            .ToListAsync();
    }

    public async Task<IEnumerable<PaymentEntity>> FindByStatusAsync(string status)
    {
        return await Context.Set<PaymentEntity>()
            .Where(p => p.Status == status)
            .ToListAsync();
    }
}
