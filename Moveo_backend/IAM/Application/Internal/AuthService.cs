using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moveo_backend.IAM.Domain.Model.Commands;
using Moveo_backend.IAM.Domain.Services;
using Moveo_backend.IAM.Infrastructure.Hashing;
using Moveo_backend.IAM.Infrastructure.Tokens;
using Moveo_backend.IAM.Interfaces.REST.Resources;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Moveo_backend.UserManagement.Domain.Model.Aggregates;
using Moveo_backend.UserManagement.Domain.Model.Commands;

namespace Moveo_backend.IAM.Application.Internal;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IHashingService _hashingService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        AppDbContext context,
        ITokenService tokenService,
        IHashingService hashingService,
        IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _tokenService = tokenService;
        _hashingService = hashingService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponse?> LoginAsync(LoginCommand command)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == command.Email.ToLower());

        if (user == null)
            return null;

        if (!_hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return null;

        return await GenerateAuthResponse(user);
    }

    public async Task<AuthResponse?> RegisterAsync(RegisterCommand command)
    {
        // Check if user already exists
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == command.Email.ToLower());

        if (existingUser != null)
            return null;

        var hashedPassword = _hashingService.HashPassword(command.Password);

        var user = new User(new CreateUserCommand(
            FirstName: command.FirstName,
            LastName: command.LastName,
            Email: command.Email,
            Password: hashedPassword,
            Phone: command.Phone ?? string.Empty,
            Dni: command.Dni ?? string.Empty,
            LicenseNumber: command.LicenseNumber ?? string.Empty,
            Role: command.Role
        ));

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return await GenerateAuthResponse(user);
    }

    public async Task<AuthResponse?> RefreshTokenAsync(RefreshTokenCommand command)
    {
        var userId = _tokenService.GetUserIdFromToken(command.AccessToken);
        if (userId == null)
            return null;

        var user = await _context.Users.FindAsync(userId.Value);
        if (user == null)
            return null;

        if (user.RefreshToken != command.RefreshToken || 
            user.RefreshTokenExpiryTime == null || 
            user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return null;

        return await GenerateAuthResponse(user);
    }

    public async Task<bool> LogoutAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return false;

        user.ClearRefreshToken();
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<AuthenticatedUserResource?> GetCurrentUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return null;

        return MapToAuthenticatedUser(user);
    }

    public async Task<bool> ChangePasswordAsync(int userId, AuthChangePasswordCommand command)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return false;

        if (!_hashingService.VerifyPassword(command.CurrentPassword, user.PasswordHash))
            return false;

        user.ChangePassword(_hashingService.HashPassword(command.NewPassword));
        await _context.SaveChangesAsync();

        return true;
    }

    private async Task<AuthResponse> GenerateAuthResponse(User user)
    {
        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.EmailAddress, user.RoleName);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

        // Save refresh token to user
        user.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays));
        await _context.SaveChangesAsync();

        return new AuthResponse
        {
            Token = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt,
            User = MapToAuthenticatedUser(user)
        };
    }

    private static AuthenticatedUserResource MapToAuthenticatedUser(User user)
    {
        return new AuthenticatedUserResource
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.EmailAddress,
            Phone = user.Phone,
            Dni = user.Dni,
            LicenseNumber = user.LicenseNumber,
            Role = user.RoleName,
            Address = user.Address
        };
    }
}
