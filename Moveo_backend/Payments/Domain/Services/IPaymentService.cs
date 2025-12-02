using Moveo_backend.Payments.Domain.Model.Aggregates;
using Moveo_backend.Payments.Domain.Model.Commands;

namespace Moveo_backend.Payments.Domain.Services;

public interface IPaymentService
{
    Task<Payment?> GetByIdAsync(Guid id);
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<IEnumerable<Payment>> GetByPayerIdAsync(int payerId);
    Task<IEnumerable<Payment>> GetByRecipientIdAsync(int recipientId);
    Task<IEnumerable<Payment>> GetByRentalIdAsync(Guid rentalId);
    Task<IEnumerable<Payment>> GetByStatusAsync(string status);
    Task<Payment> CreatePaymentAsync(CreatePaymentCommand command);
    Task<Payment> MarkAsPaidAsync(Guid paymentId, string transactionId);
    Task<Payment> MarkAsFailedAsync(Guid paymentId, string reason);
    Task<Payment> RefundPaymentAsync(Guid paymentId);
}
