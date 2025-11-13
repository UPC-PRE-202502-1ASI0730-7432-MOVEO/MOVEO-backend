using Moveo_backend.UserManagement.Domain.Model.ValueObjects;

namespace Moveo_backend.UserManagement.Domain.Model.Commands;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone,
    string Dni,
    string LicenseNumber,
    string Role,
    string Address,
    UserPreferences Preferences);