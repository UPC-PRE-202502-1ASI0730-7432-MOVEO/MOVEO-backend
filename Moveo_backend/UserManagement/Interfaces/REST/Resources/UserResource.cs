using System.Text.Json.Serialization;
using Moveo_backend.UserManagement.Domain.Model.ValueObjects;

namespace Moveo_backend.UserManagement.Interfaces.REST.Resources;

public class UserResource
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    [JsonIgnore] // Never expose password in API responses
    public string Password { get; set; } = "";

    public string Phone { get; set; } = string.Empty;
    public string Dni { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string Role { get; set; } = "renter";
    public string Address { get; set; } = string.Empty;
    public UserPreferences? Preferences { get; set; }
    
    // Refresh token fields for JWT authentication
    [JsonIgnore]
    public string? RefreshToken { get; set; }
    
    [JsonIgnore]
    public DateTime? RefreshTokenExpiryTime { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}