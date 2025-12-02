using System.Security.Claims;

namespace Moveo_backend.IAM.Domain.Services;

public interface ITokenService
{
    string GenerateAccessToken(int userId, string email, string role);
    string GenerateRefreshToken();
    ClaimsPrincipal? ValidateToken(string token);
    int? GetUserIdFromToken(string token);
}
