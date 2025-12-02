namespace Moveo_backend.Notifications.Domain.Model.Aggregates;

public class Notification
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Type { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
    public bool Read { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid? RelatedId { get; private set; }
    public string? RelatedType { get; private set; }

    protected Notification()
    {
        Type = string.Empty;
        Title = string.Empty;
        Body = string.Empty;
    }

    public Notification(
        int userId,
        string type,
        string title,
        string body,
        Guid? relatedId = null,
        string? relatedType = null)
    {
        UserId = userId;
        Type = type;
        Title = title;
        Body = body;
        Read = false;
        CreatedAt = DateTime.UtcNow;
        RelatedId = relatedId;
        RelatedType = relatedType;
    }

    public void MarkAsRead()
    {
        Read = true;
    }

    public void MarkAsUnread()
    {
        Read = false;
    }
}
