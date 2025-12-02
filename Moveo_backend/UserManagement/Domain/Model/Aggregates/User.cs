using Moveo_backend.UserManagement.Domain.Model.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moveo_backend.UserManagement.Domain.Model.Aggregates;

public partial class User
{
    [Key]
    public int Id { get; private set; }

    public PersonName Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public Password Password { get; private set; }
    public UserRole Role { get; private set; }
    
    public string Phone { get; private set; }
    public string Dni { get; private set; }
    public string LicenseNumber { get; private set; }
    public string Address { get; private set; }
    
    public UserPreferences Preferences { get; private set; }
    
    // Refresh token support for JWT
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; set; }

    [NotMapped]
    public string FullName => Name.FullName;
    [NotMapped]
    public string EmailAddress => Email.Address;
    [NotMapped]
    public string RoleName => Role.Value.ToString();
    
    public User(
        string firstName,
        string lastName,
        string email,
        string password,
        string role,
        string phone,
        string dni,
        string licenseNumber,
        string address,
        UserPreferences preferences)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
        Password = new Password(password);
        Role = new UserRole(role);
        Phone = phone;
        Dni = dni;
        LicenseNumber = licenseNumber;
        Address = address;
        Preferences = preferences;
        CreatedAt = DateTime.UtcNow;
    }
    
    public User()
    {
        Name = new PersonName();
        Email = new EmailAddress();
        Password = new Password();
        Role = new UserRole();
        Preferences = new UserPreferences("es", true, true, false, false, 1, false);
        Phone = string.Empty;
        Dni = string.Empty;
        LicenseNumber = string.Empty;
        Address = string.Empty;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void Update(
        string firstName,
        string lastName,
        string phone,
        string address,
        UserPreferences preferences)
    {
        Name = new PersonName(firstName, lastName);
        Phone = phone;
        Address = address;
        Preferences = preferences;
        UpdatedAt = DateTime.UtcNow;
    }


    public void ChangePassword(string newPassword)
    {
        Password = new Password(newPassword);
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePreferences(UserPreferences newPreferences)
    {
        Preferences = newPreferences;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void SetRefreshToken(string refreshToken, DateTime expiryTime)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = expiryTime;
    }
    
    public void ClearRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiryTime = null;
    }
}
