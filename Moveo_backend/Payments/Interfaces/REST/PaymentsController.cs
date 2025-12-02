using Microsoft.AspNetCore.Mvc;
using Moveo_backend.Payments.Domain.Services;
using Moveo_backend.Payments.Interfaces.REST.Resources;
using Moveo_backend.Payments.Interfaces.REST.Transform;

namespace Moveo_backend.Payments.Interfaces.REST;

[ApiController]
[Route("api/v1/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int? payerId,
        [FromQuery] int? recipientId,
        [FromQuery] string? rentalId,
        [FromQuery] string? status)
    {
        IEnumerable<Payments.Domain.Model.Aggregates.Payment> payments;

        if (payerId.HasValue)
        {
            payments = await _paymentService.GetByPayerIdAsync(payerId.Value);
        }
        else if (recipientId.HasValue)
        {
            payments = await _paymentService.GetByRecipientIdAsync(recipientId.Value);
        }
        else if (!string.IsNullOrEmpty(rentalId) && Guid.TryParse(rentalId, out var rentalGuid))
        {
            payments = await _paymentService.GetByRentalIdAsync(rentalGuid);
        }
        else if (!string.IsNullOrEmpty(status))
        {
            payments = await _paymentService.GetByStatusAsync(status);
        }
        else
        {
            payments = await _paymentService.GetAllAsync();
        }

        var resources = payments.Select(PaymentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var payment = await _paymentService.GetByIdAsync(id);
        if (payment == null) return NotFound();
        return Ok(PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentResource resource)
    {
        var command = CreatePaymentCommandFromResourceAssembler.ToCommand(resource);
        var payment = await _paymentService.CreatePaymentAsync(command);
        var result = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);
        return CreatedAtAction(nameof(GetById), new { id = payment.Id }, result);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdatePaymentStatusResource resource)
    {
        try
        {
            Payments.Domain.Model.Aggregates.Payment payment;

            switch (resource.Status.ToLower())
            {
                case "completed":
                case "paid":
                    if (string.IsNullOrEmpty(resource.TransactionId))
                        return BadRequest("TransactionId is required for completed status");
                    payment = await _paymentService.MarkAsPaidAsync(id, resource.TransactionId);
                    break;
                case "failed":
                    payment = await _paymentService.MarkAsFailedAsync(id, resource.FailureReason ?? "Unknown error");
                    break;
                case "refunded":
                    payment = await _paymentService.RefundPaymentAsync(id);
                    break;
                default:
                    return BadRequest($"Invalid status: {resource.Status}");
            }

            return Ok(PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
