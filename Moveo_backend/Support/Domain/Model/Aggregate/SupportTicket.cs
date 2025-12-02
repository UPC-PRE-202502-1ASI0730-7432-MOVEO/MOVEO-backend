using Moveo_backend.Support.Domain.Model.Commands;

namespace Moveo_backend.Support.Domain.Model.Aggregate;

public class SupportTicket
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Subject { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; } // technical, billing, general, rental_issue, etc.
    public string Status { get; private set; } // open, in_progress, resolved, closed
    public string Priority { get; private set; } // low, medium, high, urgent
    public int? AssignedToId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? ResolvedAt { get; private set; }
    
    // Navigation property
    public ICollection<TicketMessage> Messages { get; private set; } = new List<TicketMessage>();

    protected SupportTicket() 
    {
        Subject = string.Empty;
        Description = string.Empty;
        Category = string.Empty;
        Status = string.Empty;
        Priority = string.Empty;
    }

    public SupportTicket(CreateSupportTicketCommand command)
    {
        UserId = command.UserId;
        Subject = command.Subject;
        Description = command.Description;
        Category = command.Category;
        Status = "open";
        Priority = command.Priority ?? "medium";
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(UpdateSupportTicketCommand command)
    {
        if (command.Status is not null) Status = command.Status;
        if (command.Priority is not null) Priority = command.Priority;
        if (command.AssignedToId.HasValue) AssignedToId = command.AssignedToId.Value;
        
        if (command.Status == "resolved" && ResolvedAt is null)
        {
            ResolvedAt = DateTime.UtcNow;
        }
        
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddMessage(TicketMessage message)
    {
        Messages.Add(message);
        UpdatedAt = DateTime.UtcNow;
    }
}
