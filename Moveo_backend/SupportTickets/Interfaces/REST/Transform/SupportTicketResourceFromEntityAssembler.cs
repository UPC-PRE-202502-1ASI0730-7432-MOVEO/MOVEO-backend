using Moveo_backend.SupportTickets.Domain.Model.Aggregates;
using Moveo_backend.SupportTickets.Interfaces.REST.Resources;

namespace Moveo_backend.SupportTickets.Interfaces.REST.Transform;

public static class SupportTicketResourceFromEntityAssembler
{
    public static SupportTicketResource ToResourceFromEntity(SupportTicket entity)
    {
        return new SupportTicketResource(
            entity.Id,
            entity.UserId,
            entity.Subject,
            entity.Description,
            entity.Category,
            entity.Priority,
            entity.Status,
            entity.RelatedRentalId,
            entity.RelatedVehicleId,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.ResolvedAt,
            entity.Resolution,
            entity.AssignedToId
        );
    }
}
