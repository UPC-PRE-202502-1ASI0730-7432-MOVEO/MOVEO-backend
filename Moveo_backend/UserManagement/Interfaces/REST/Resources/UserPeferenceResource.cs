namespace Moveo_backend.UserManagement.Interfaces.REST.Resources;

public class UserPreferencesResource
{
    public string Language { get; set; } = "es";
    public bool EmailNotifications { get; set; } = true;
    public bool PushNotifications { get; set; } = true;
    public bool SmsNotifications { get; set; } = false;
    public bool AutoAcceptRentals { get; set; } = false;
    public int MinimumRentalDays { get; set; } = 1;
    public bool InstantBooking { get; set; } = false;
}