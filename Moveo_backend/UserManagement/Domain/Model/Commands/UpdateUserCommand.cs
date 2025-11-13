namespace Moveo_backend.UserManagement.Domain.Model.Commands;

public record UpdateUserCommand(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Role,
    string Phone,
    string Dni,
    string LicenseNumber,
    string Address,
    UserPreferencesDto Preferences
);

public record UserPreferencesDto(
    string Language,
    bool EmailNotifications,
    bool PushNotifications,
    bool SmsNotifications,
    bool AutoAcceptRentals,
    int MinimumRentalDays,
    bool InstantBooking
);