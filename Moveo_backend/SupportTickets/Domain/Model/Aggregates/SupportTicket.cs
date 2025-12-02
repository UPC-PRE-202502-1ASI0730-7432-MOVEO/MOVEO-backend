namespace Moveo_backend.SupportTickets.Domain.Model.Aggregates;

public class SupportTicket
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Subject { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; } // "rental", "payment", "vehicle", "account", "other"
    public string Priority { get; private set; } // "low", "medium", "high", "urgent"
    public string Status { get; private set; } // "open", "in_progress", "resolved", "closed"
    public Guid? RelatedRentalId { get; private set; }
    public Guid? RelatedVehicleId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? ResolvedAt { get; private set; }
    public string? Resolution { get; private set; }
    public int? AssignedToId { get; private set; }

    // Constructor for EF Core
    private SupportTicket() 
    {
        Subject = string.Empty;
        Description = string.Empty;
        Category = "other";
        Priority = "medium";
        Status = "open";
    }

    public SupportTicket(
        int userId,
        string subject,
        string description,
        string category,
        string priority,
        Guid? relatedRentalId = null,
        Guid? relatedVehicleId = null)
    {
        UserId = userId;
        Subject = subject;
        Description = description;
        Category = category;
        Priority = priority;
        Status = "open";
        RelatedRentalId = relatedRentalId;
        RelatedVehicleId = relatedVehicleId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string subject, string description, string category, string priority)
    {
        Subject = subject;
        Description = description;
        Category = category;
        Priority = priority;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignTo(int adminId)
    {
        AssignedToId = adminId;
        Status = "in_progress";
        UpdatedAt = DateTime.UtcNow;
    }

    public void Resolve(string resolution)
    {
        Resolution = resolution;
        Status = "resolved";
        ResolvedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Close()
    {
        Status = "closed";
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reopen()
    {
        Status = "open";
        Resolution = null;
        ResolvedAt = null;
        UpdatedAt = DateTime.UtcNow;
    }
}
