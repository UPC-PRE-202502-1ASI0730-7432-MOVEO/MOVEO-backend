using Moveo_backend.UserManagement.Domain.Model.Commands;
using Moveo_backend.UserManagement.Domain.Model.ValueObjects;
using Moveo_backend.UserManagement.Interfaces.REST.Resources;

namespace Moveo_backend.UserManagement.Interfaces.REST.Transform;

public static class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(UpdateUserResource resource, int userId)
        {
            // Creamos el DTO de preferencias; si el frontend no manda preferencias, usamos default
            var preferencesDto = resource.Preferences != null
                ? new UserPreferencesDto(
                    resource.Preferences.Language,
                    resource.Preferences.EmailNotifications,
                    resource.Preferences.PushNotifications,
                    resource.Preferences.SmsNotifications,
                    resource.Preferences.AutoAcceptRentals,
                    resource.Preferences.MinimumRentalDays,
                    resource.Preferences.InstantBooking
                  )
                : new UserPreferencesDto("es", true, true, false, false, 1, false);

            return new UpdateUserCommand(
                userId,
                resource.FirstName,
                resource.LastName,
                resource.Email,
                resource.Password,
                resource.Role,
                resource.Phone ?? string.Empty,
                resource.Dni ?? string.Empty,
                resource.LicenseNumber ?? string.Empty,
                resource.Address ?? string.Empty,
                preferencesDto
            );
        }
    }
