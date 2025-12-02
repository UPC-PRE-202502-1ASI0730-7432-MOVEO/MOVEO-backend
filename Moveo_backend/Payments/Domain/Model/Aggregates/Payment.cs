namespace Moveo_backend.Payments.Domain.Model.Aggregates;

public class Payment
{
    public Guid Id { get; private set; }
    public Guid RentalId { get; private set; }
    public int PayerId { get; private set; }
    public int RecipientId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = "PEN";
    public string Status { get; private set; } = "pending";
    public string PaymentMethod { get; private set; } = string.Empty;
    public string? TransactionId { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? PaidAt { get; private set; }
    public DateTime? FailedAt { get; private set; }
    public string? FailureReason { get; private set; }

    private Payment() { }

    public Payment(
        Guid rentalId,
        int payerId,
        int recipientId,
        decimal amount,
        string currency,
        string paymentMethod,
        string? description = null)
    {
        Id = Guid.NewGuid();
        RentalId = rentalId;
        PayerId = payerId;
        RecipientId = recipientId;
        Amount = amount;
        Currency = currency;
        PaymentMethod = paymentMethod;
        Description = description;
        Status = "pending";
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsPaid(string transactionId)
    {
        if (Status != "pending")
            throw new InvalidOperationException("Can only mark pending payments as paid");

        Status = "completed";
        TransactionId = transactionId;
        PaidAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string reason)
    {
        if (Status != "pending")
            throw new InvalidOperationException("Can only mark pending payments as failed");

        Status = "failed";
        FailureReason = reason;
        FailedAt = DateTime.UtcNow;
    }

    public void Refund()
    {
        if (Status != "completed")
            throw new InvalidOperationException("Can only refund completed payments");

        Status = "refunded";
    }
}
