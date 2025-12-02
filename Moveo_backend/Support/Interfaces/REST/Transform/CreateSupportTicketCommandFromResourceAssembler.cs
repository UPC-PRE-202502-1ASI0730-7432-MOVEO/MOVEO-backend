using Moveo_backend.Support.Domain.Model.Commands;
using Moveo_backend.Support.Interfaces.REST.Resources;

namespace Moveo_backend.Support.Interfaces.REST.Transform;

public static class CreateSupportTicketCommandFromResourceAssembler
{
    public static CreateSupportTicketCommand ToCommandFromResource(CreateSupportTicketResource resource)
    {
        return new CreateSupportTicketCommand(
            resource.UserId,
            resource.Subject,
            resource.Description,
            resource.Category,
            resource.Priority
        );
    }
}
