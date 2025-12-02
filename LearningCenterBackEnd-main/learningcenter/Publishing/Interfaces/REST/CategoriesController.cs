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
[SwaggerTag("Available Category Endpoints")]
public class CategoriesController(
    ICategoryCommandService categoryCommandService,
    ICategoryQueryService categoryQueryService) : ControllerBase
{
    [HttpGet("{categoryId:int}")]
    [SwaggerOperation(
        Summary = "Get All categories",
        Description = "Get All Categories",
        OperationId = "GetAllCategories"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The  list of categories", typeof(CreateCategoryResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No categories found")]
    public async Task<IActionResult> GetCategoryById(int categoryId)
    {
        var getCategoryByIdQuery = new GetCategoryByIdQuery(categoryId);
        var category = await categoryQueryService.Handle(getCategoryByIdQuery);
        if (category is null) return NotFound();
        var resource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new category",
        Description = "Create a new category",
        OperationId = "CreateCategory")]
    [SwaggerResponse(StatusCodes.Status201Created, "The Category was created", typeof(CategoryResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The category could not be created")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryResource resource) 
    {
        var createCategoryCommand = CreateCategoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var category = await categoryCommandService.Handle(createCategoryCommand);
        if (category is null) return BadRequest();
        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return CreatedAtAction(nameof(GetCategoryById), new { categoryId = category.Id }, categoryResource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Categories",
        Description = "Get All Categories ",
        OperationId = "GetAllCategories"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The List Of Categories", typeof(IEnumerable<CategoryResource>))]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await categoryQueryService.Handle(new GetAllCategoriesQuery());
        var categoryResources = categories.Select(CategoryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(categoryResources);
    }
}