using Moveo_backend.UserManagement.Domain.Model.ValueObjects;

namespace Moveo_backend.UserManagement.Interfaces.REST.Resources;

public class CreateUserResource
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "renter";
    public string Phone { get; set; } = string.Empty;
    public string Dni { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public UserPreferencesResource Preferences { get; set; } = new();
}