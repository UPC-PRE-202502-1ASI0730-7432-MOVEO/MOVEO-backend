using System.Text.Json.Serialization;

namespace Moveo_backend.Support.Interfaces.REST.Resources;

public class CreateSupportTicketResource
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    
    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    
    [JsonPropertyName("category")]
    public string? Category { get; set; }
    
    [JsonPropertyName("priority")]
    public string? Priority { get; set; }
    
    // Helper to get category with default
    public string GetCategory() => Category ?? "general";
    
    // Helper to get priority with default
    public string GetPriority() => Priority ?? "medium";
}
