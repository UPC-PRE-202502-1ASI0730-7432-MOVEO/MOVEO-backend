using Moveo_backend.Payments.Domain.Model.Aggregates;

namespace Moveo_backend.Payments.Domain.Repositories;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid id);
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<IEnumerable<Payment>> GetByPayerIdAsync(int payerId);
    Task<IEnumerable<Payment>> GetByRecipientIdAsync(int recipientId);
    Task<IEnumerable<Payment>> GetByRentalIdAsync(Guid rentalId);
    Task<IEnumerable<Payment>> GetByStatusAsync(string status);
    Task AddAsync(Payment payment);
    Task UpdateAsync(Payment payment);
}
