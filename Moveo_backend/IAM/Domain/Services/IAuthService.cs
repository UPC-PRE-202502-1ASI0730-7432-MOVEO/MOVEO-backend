using Moveo_backend.IAM.Domain.Model.Commands;
using Moveo_backend.IAM.Interfaces.REST.Resources;

namespace Moveo_backend.IAM.Domain.Services;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginCommand command);
    Task<AuthResponse?> RegisterAsync(RegisterCommand command);
    Task<AuthResponse?> RefreshTokenAsync(RefreshTokenCommand command);
    Task<bool> LogoutAsync(int userId);
    Task<AuthenticatedUserResource?> GetCurrentUserAsync(int userId);
    Task<bool> ChangePasswordAsync(int userId, AuthChangePasswordCommand command);
}
