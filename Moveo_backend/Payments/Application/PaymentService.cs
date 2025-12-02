using Moveo_backend.Payments.Domain.Model.Aggregates;
using Moveo_backend.Payments.Domain.Model.Commands;
using Moveo_backend.Payments.Domain.Repositories;
using Moveo_backend.Payments.Domain.Services;

namespace Moveo_backend.Payments.Application;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Payment?> GetByIdAsync(Guid id)
    {
        return await _paymentRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _paymentRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Payment>> GetByPayerIdAsync(int payerId)
    {
        return await _paymentRepository.GetByPayerIdAsync(payerId);
    }

    public async Task<IEnumerable<Payment>> GetByRecipientIdAsync(int recipientId)
    {
        return await _paymentRepository.GetByRecipientIdAsync(recipientId);
    }

    public async Task<IEnumerable<Payment>> GetByRentalIdAsync(Guid rentalId)
    {
        return await _paymentRepository.GetByRentalIdAsync(rentalId);
    }

    public async Task<IEnumerable<Payment>> GetByStatusAsync(string status)
    {
        return await _paymentRepository.GetByStatusAsync(status);
    }

    public async Task<Payment> CreatePaymentAsync(CreatePaymentCommand command)
    {
        var payment = new Payment(
            command.RentalId,
            command.PayerId,
            command.RecipientId,
            command.Amount,
            command.Currency,
            command.PaymentMethod,
            command.Description
        );

        await _paymentRepository.AddAsync(payment);
        return payment;
    }

    public async Task<Payment> MarkAsPaidAsync(Guid paymentId, string transactionId)
    {
        var payment = await _paymentRepository.GetByIdAsync(paymentId);
        if (payment == null)
            throw new KeyNotFoundException($"Payment with id {paymentId} not found");

        payment.MarkAsPaid(transactionId);
        await _paymentRepository.UpdateAsync(payment);
        return payment;
    }

    public async Task<Payment> MarkAsFailedAsync(Guid paymentId, string reason)
    {
        var payment = await _paymentRepository.GetByIdAsync(paymentId);
        if (payment == null)
            throw new KeyNotFoundException($"Payment with id {paymentId} not found");

        payment.MarkAsFailed(reason);
        await _paymentRepository.UpdateAsync(payment);
        return payment;
    }

    public async Task<Payment> RefundPaymentAsync(Guid paymentId)
    {
        var payment = await _paymentRepository.GetByIdAsync(paymentId);
        if (payment == null)
            throw new KeyNotFoundException($"Payment with id {paymentId} not found");

        payment.Refund();
        await _paymentRepository.UpdateAsync(payment);
        return payment;
    }
}
