using Microsoft.AspNetCore.Mvc;
using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Model.Queries;
using Moveo_backend.Rental.Domain.Services;
using Moveo_backend.Rental.Interfaces.REST.Resources;
using Moveo_backend.Rental.Interfaces.REST.Transform;

namespace Moveo_backend.Rental.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewCommandService _reviewCommandService;
    private readonly IReviewQueryService _reviewQueryService;

    public ReviewsController(
        IReviewCommandService reviewCommandService,
        IReviewQueryService reviewQueryService)
    {
        _reviewCommandService = reviewCommandService;
        _reviewQueryService = reviewQueryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReviews()
    {
        var query = new GetAllReviewsQuery();
        var reviews = await _reviewQueryService.Handle(query);
        var resources = reviews.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetReviewById(int id)
    {
        var query = new GetReviewByIdQuery(id);
        var review = await _reviewQueryService.Handle(query);
        if (review == null) return NotFound();
        var resource = ReviewResourceFromEntityAssembler.ToResourceFromEntity(review);
        return Ok(resource);
    }

    [HttpGet("rental/{rentalId:guid}")]
    public async Task<IActionResult> GetReviewsByRentalId(Guid rentalId)
    {
        var query = new GetReviewsByRentalIdQuery(rentalId);
        var reviews = await _reviewQueryService.Handle(query);
        var resources = reviews.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("reviewer/{reviewerId:guid}")]
    public async Task<IActionResult> GetReviewsByReviewerId(Guid reviewerId)
    {
        var query = new GetReviewsByReviewerIdQuery(reviewerId);
        var reviews = await _reviewQueryService.Handle(query);
        var resources = reviews.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("reviewee/{revieweeId:guid}")]
    public async Task<IActionResult> GetReviewsByRevieweeId(Guid revieweeId)
    {
        var query = new GetReviewsByRevieweeIdQuery(revieweeId);
        var reviews = await _reviewQueryService.Handle(query);
        var resources = reviews.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewResource resource)
    {
        var command = CreateReviewCommandFromResourceAssembler.ToCommandFromResource(resource);
        var review = await _reviewCommandService.Handle(command);
        if (review == null) return BadRequest();
        var reviewResource = ReviewResourceFromEntityAssembler.ToResourceFromEntity(review);
        return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, reviewResource);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewResource resource)
    {
        var command = UpdateReviewCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var review = await _reviewCommandService.Handle(command);
        if (review == null) return NotFound();
        var reviewResource = ReviewResourceFromEntityAssembler.ToResourceFromEntity(review);
        return Ok(reviewResource);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var command = new DeleteReviewCommand(id);
        var result = await _reviewCommandService.Handle(command);
        if (!result) return NotFound();
        return NoContent();
    }
}
