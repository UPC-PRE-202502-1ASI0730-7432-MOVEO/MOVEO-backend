namespace Moveo_backend.IAM.Interfaces.REST.Resources;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public AuthenticatedUserResource User { get; set; } = null!;
}
