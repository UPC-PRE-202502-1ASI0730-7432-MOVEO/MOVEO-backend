using Microsoft.AspNetCore.Mvc;
using Moveo_backend.AdventureRoutes.Domain.Model.Commands;
using Moveo_backend.AdventureRoutes.Domain.Services;
using Moveo_backend.AdventureRoutes.Interfaces.REST.Resources;
using Moveo_backend.AdventureRoutes.Interfaces.REST.Transform;

namespace Moveo_backend.AdventureRoutes.Interfaces.REST;

[ApiController]
[Route("api/v1/adventure-routes")]
public class AdventureRoutesController : ControllerBase
{
    private readonly IAdventureRouteService _routeService;

    public AdventureRoutesController(IAdventureRouteService routeService)
    {
        _routeService = routeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var routes = await _routeService.GetActiveAsync();
        return Ok(routes.Select(AdventureRouteResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllIncludingInactive()
    {
        var routes = await _routeService.GetAllAsync();
        return Ok(routes.Select(AdventureRouteResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var route = await _routeService.GetByIdAsync(id);
        if (route == null) return NotFound();
        return Ok(AdventureRouteResourceFromEntityAssembler.ToResourceFromEntity(route));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAdventureRouteResource resource)
    {
        var command = new CreateAdventureRouteCommand(
            resource.Name,
            resource.Description,
            resource.Difficulty,
            resource.Distance,
            resource.EstimatedTime ?? "1 hour",
            resource.Waypoints?.Select(w => new WaypointDto(w.Lat, w.Lng, w.Name)).ToList() ?? new List<WaypointDto>(),
            resource.Images ?? new List<string>()
        );
        var route = await _routeService.CreateAsync(command);
        var result = AdventureRouteResourceFromEntityAssembler.ToResourceFromEntity(route);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAdventureRouteResource resource)
    {
        var command = new UpdateAdventureRouteCommand(
            id,
            resource.Name,
            resource.Description,
            resource.Difficulty,
            resource.Distance,
            resource.EstimatedTime ?? "1 hour",
            resource.Waypoints?.Select(w => new WaypointDto(w.Lat, w.Lng, w.Name)).ToList() ?? new List<WaypointDto>(),
            resource.Images ?? new List<string>()
        );
        var route = await _routeService.UpdateAsync(command);
        if (route == null) return NotFound();
        return Ok(AdventureRouteResourceFromEntityAssembler.ToResourceFromEntity(route));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _routeService.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
