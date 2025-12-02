using Moveo_backend.Support.Domain.Model.Aggregate;
using Moveo_backend.Support.Interfaces.REST.Resources;

namespace Moveo_backend.Support.Interfaces.REST.Transform;

public static class SupportTicketResourceFromEntityAssembler
{
    public static SupportTicketResource ToResourceFromEntity(SupportTicket entity, bool includeMessages = false)
    {
        var messages = includeMessages && entity.Messages.Any()
            ? entity.Messages.Select(TicketMessageResourceFromEntityAssembler.ToResourceFromEntity)
            : null;

        return new SupportTicketResource(
            entity.Id,
            entity.UserId,
            entity.Subject,
            entity.Description,
            entity.Category,
            entity.Status,
            entity.Priority,
            entity.AssignedToId,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.ResolvedAt,
            messages
        );
    }
}
