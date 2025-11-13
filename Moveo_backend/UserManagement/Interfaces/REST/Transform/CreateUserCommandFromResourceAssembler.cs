using Moveo_backend.UserManagement.Domain.Model.Commands;
using Moveo_backend.UserManagement.Domain.Model.ValueObjects;
using Moveo_backend.UserManagement.Interfaces.REST.Resources;

namespace Moveo_backend.UserManagement.Interfaces.REST.Transform;

public static class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource)
    {
        // Convertimos el DTO del frontend a VO (Value Object)
        var preferences = resource.Preferences != null 
            ? new UserPreferences(
                resource.Preferences.Language,
                resource.Preferences.EmailNotifications,
                resource.Preferences.PushNotifications,
                resource.Preferences.SmsNotifications,
                resource.Preferences.AutoAcceptRentals,
                resource.Preferences.MinimumRentalDays,
                resource.Preferences.InstantBooking
            )
            : new UserPreferences("es", true, true, false, false, 1, false);

        return new CreateUserCommand(
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Password,
            resource.Phone ?? string.Empty,
            resource.Dni ?? string.Empty,
            resource.LicenseNumber ?? string.Empty,
            resource.Role,
            resource.Address ?? string.Empty,
            preferences  // VO correcto
        );
    }
}