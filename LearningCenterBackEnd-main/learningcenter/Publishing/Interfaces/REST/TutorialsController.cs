using System.Net.Mime;
using learningcenter.Publishing.Domain.Model.Queries;
using learningcenter.Publishing.Domain.Services;
using learningcenter.Publishing.Interfaces.REST.Resources;
using learningcenter.Publishing.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace learningcenter.Publishing.Interfaces.REST;
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Tutorial endpoints")]
public class TutorialsController
(ITutorialCommandServices tutorialCommandServices,
    ITutorialQueryService tutorialQueryService
    ): ControllerBase
{
    [HttpGet("{tutorialId:int}")]
    [SwaggerOperation(
        Summary = "Get  a Tutorial by its id",
        Description = "Gets a Tutorial by its id",
        OperationId = "GetTutorialById"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The Tutorial was found ", typeof(TutorialResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Tutorial was not found")]
    public async Task<IActionResult> GetTutorialById([FromRoute] int tutorialId)
    {
        var tutorial = await tutorialQueryService.Handle(new GetTutorialByIdQuery(tutorialId));
        if (tutorial is null) return NotFound();
        var tutorialResource = TutorialResourceFromEntityAssembler.ToResourceFromEntity(tutorial);
        return Ok(tutorialResource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create Tutorial",
        Description = "Create Tutorial",
        OperationId = "CreateTutorial")]
    [SwaggerResponse(StatusCodes.Status201Created, "The tutorial was created", typeof(TutorialResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "the tutorial was not created")]
    public async Task<IActionResult> CreateTutorial([FromBody] CreateTutorialResource resource)
    {
        var createTutorialCommand = CreateTutorialCommandFromResourceAssembler.ToCommmandFromResource(resource);
        var tutorial = await tutorialCommandServices.Handle(createTutorialCommand);
        if (tutorial is null) return BadRequest();
        var tutorialResource = TutorialResourceFromEntityAssembler.ToResourceFromEntity(tutorial);
        return CreatedAtAction(nameof(GetTutorialById), new { tutorialId = tutorial.Id }, tutorialResource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Tutorial",
        Description = "GetAll Tutorial",
        OperationId = "GetAllTutorial")]
    [SwaggerResponse(StatusCodes.Status200OK, "The tutorials were found", typeof(TutorialResource))]
    public async Task<IActionResult> GetAllTutorial()
    {
        var getAllTutorialsQuery = new GetAllTutorialsQuery();
        var tutorials = await tutorialQueryService.Handle(getAllTutorialsQuery);
        var tutorialResources = tutorials.Select(TutorialResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(tutorialResources);
    }
}