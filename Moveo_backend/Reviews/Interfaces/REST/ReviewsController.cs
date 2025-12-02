using Microsoft.AspNetCore.Mvc;
using Moveo_backend.Reviews.Domain.Services;
using Moveo_backend.Reviews.Interfaces.REST.Resources;
using Moveo_backend.Reviews.Interfaces.REST.Transform;

namespace Moveo_backend.Reviews.Interfaces.REST;

[ApiController]
[Route("api/v1/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? vehicleId,
        [FromQuery] int? reviewerId,
        [FromQuery] int? ownerId)
    {
        IEnumerable<Reviews.Domain.Model.Aggregates.Review> reviews;

        if (!string.IsNullOrEmpty(vehicleId) && Guid.TryParse(vehicleId, out var vehicleGuid))
        {
            reviews = await _reviewService.GetByVehicleIdAsync(vehicleGuid);
        }
        else if (reviewerId.HasValue)
        {
            reviews = await _reviewService.GetByReviewerIdAsync(reviewerId.Value);
        }
        else if (ownerId.HasValue)
        {
            reviews = await _reviewService.GetByOwnerIdAsync(ownerId.Value);
        }
        else
        {
            reviews = await _reviewService.GetAllAsync();
        }

        var resources = reviews.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var review = await _reviewService.GetByIdAsync(id);
        if (review == null) return NotFound();
        return Ok(ReviewResourceFromEntityAssembler.ToResourceFromEntity(review));
    }

    [HttpGet("rental/{rentalId:guid}")]
    public async Task<IActionResult> GetByRentalId(Guid rentalId)
    {
        var review = await _reviewService.GetByRentalIdAsync(rentalId);
        if (review == null) return NotFound();
        return Ok(ReviewResourceFromEntityAssembler.ToResourceFromEntity(review));
    }

    [HttpGet("vehicle/{vehicleId:guid}/average")]
    public async Task<IActionResult> GetAverageRatingForVehicle(Guid vehicleId)
    {
        var averageRating = await _reviewService.GetAverageRatingForVehicleAsync(vehicleId);
        return Ok(new { vehicleId = vehicleId.ToString(), averageRating });
    }

    [HttpGet("owner/{ownerId:int}/average")]
    public async Task<IActionResult> GetAverageRatingForOwner(int ownerId)
    {
        var averageRating = await _reviewService.GetAverageRatingForOwnerAsync(ownerId);
        return Ok(new { ownerId, averageRating });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReviewResource resource)
    {
        try
        {
            var command = CreateReviewCommandFromResourceAssembler.ToCommand(resource);
            var review = await _reviewService.CreateReviewAsync(command);
            var result = ReviewResourceFromEntityAssembler.ToResourceFromEntity(review);
            return CreatedAtAction(nameof(GetById), new { id = review.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReviewResource resource)
    {
        try
        {
            var command = UpdateReviewCommandFromResourceAssembler.ToCommand(resource);
            var review = await _reviewService.UpdateReviewAsync(id, command);
            return Ok(ReviewResourceFromEntityAssembler.ToResourceFromEntity(review));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _reviewService.DeleteReviewAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
