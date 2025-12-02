using Microsoft.AspNetCore.Mvc;
using Moveo_backend.Notifications.Domain.Model.Commands;
using Moveo_backend.Notifications.Domain.Services;
using Moveo_backend.Notifications.Interfaces.REST.Resources;
using Moveo_backend.Notifications.Interfaces.REST.Transform;

namespace Moveo_backend.Notifications.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? userId)
    {
        if (userId.HasValue)
        {
            var notifications = await _notificationService.GetByUserIdAsync(userId.Value);
            return Ok(notifications.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity));
        }
        var all = await _notificationService.GetAllAsync();
        return Ok(all.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var notification = await _notificationService.GetByIdAsync(id);
        if (notification == null) return NotFound();
        return Ok(NotificationResourceFromEntityAssembler.ToResourceFromEntity(notification));
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var notifications = await _notificationService.GetByUserIdAsync(userId);
        return Ok(notifications.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("user/{userId:int}/unread")]
    public async Task<IActionResult> GetUnreadByUserId(int userId)
    {
        var notifications = await _notificationService.GetUnreadByUserIdAsync(userId);
        return Ok(notifications.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("user/{userId:int}/unread/count")]
    public async Task<IActionResult> GetUnreadCount(int userId)
    {
        var count = await _notificationService.GetUnreadCountAsync(userId);
        return Ok(new { count });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNotificationResource resource)
    {
        var command = new CreateNotificationCommand(
            resource.UserId,
            resource.Type,
            resource.Title,
            resource.Body,
            resource.RelatedId,
            resource.RelatedType
        );
        var notification = await _notificationService.CreateNotificationAsync(command);
        var result = NotificationResourceFromEntityAssembler.ToResourceFromEntity(notification);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}/read")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var success = await _notificationService.MarkAsReadAsync(id);
        return success ? NoContent() : NotFound();
    }

    [HttpPut("user/{userId:int}/read-all")]
    public async Task<IActionResult> MarkAllAsRead(int userId)
    {
        await _notificationService.MarkAllAsReadAsync(userId);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _notificationService.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
