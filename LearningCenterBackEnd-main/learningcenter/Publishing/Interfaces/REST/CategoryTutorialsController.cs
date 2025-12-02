using System.Net.Mime;
using Cortex.Mediator;
using learningcenter.Publishing.Domain.Model.Aggregate;
using learningcenter.Publishing.Domain.Model.Queries;
using learningcenter.Publishing.Domain.Services;
using learningcenter.Publishing.Interfaces.REST.Resources;
using learningcenter.Publishing.Interfaces.REST.Transform;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace learningcenter.Publishing.Interfaces.REST;

[ApiController]
[Route("api/v1/categories/{categoryId:int}/tutorials")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Categories")]
public class CategoryTutorialsController(ITutorialQueryService tutorialQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all tutorials by category id",
        Description = "Get all tutorials by category id",
        OperationId = "GetAllTutorialsByCategoryId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of tutorials", typeof(IEnumerable<TutorialResource>))]
    public async Task<IActionResult> GetTutorialsByCategoryId(int categoryId)
    {
        var getTutorialsByCategoryIdQuery = new GetAllTutorialsByCategoryIdQuery(categoryId);
        var tutorials = await tutorialQueryService.Handle(getTutorialsByCategoryIdQuery);
        var tutorialResources = tutorials.Select(TutorialResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(tutorialResources);
    }
}