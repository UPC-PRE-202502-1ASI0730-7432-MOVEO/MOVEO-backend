using Moveo_backend.UserManagement.Domain.Model.ValueObjects;

namespace Moveo_backend.UserManagement.Interfaces.REST.Resources;

public record UpdateUserResource(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Role,
    string Phone,
    string Dni,
    string LicenseNumber,
    string Address,
    UserPreferencesResource Preferences
);