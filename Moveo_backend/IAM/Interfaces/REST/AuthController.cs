using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moveo_backend.IAM.Domain.Model.Commands;
using Moveo_backend.IAM.Domain.Services;
using Moveo_backend.IAM.Interfaces.REST.Resources;

namespace Moveo_backend.IAM.Interfaces.REST;

[ApiController]
[Route("api/v1/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Authenticate user and get JWT tokens
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            return BadRequest(new { message = "Email and password are required" });

        var command = new LoginCommand(request.Email, request.Password);
        var result = await _authService.LoginAsync(command);

        if (result == null)
            return Unauthorized(new { message = "Invalid email or password" });

        return Ok(result);
    }

    /// <summary>
    /// Register a new user account
    /// </summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            return BadRequest(new { message = "Email and password are required" });

        if (string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.LastName))
            return BadRequest(new { message = "First name and last name are required" });

        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.Phone,
            request.Dni,
            request.LicenseNumber,
            request.Address,
            request.Role,
            request.Preferences
        );

        var result = await _authService.RegisterAsync(command);

        if (result == null)
            return Conflict(new { message = "User with this email already exists" });

        return CreatedAtAction(nameof(GetCurrentUser), result);
    }

    /// <summary>
    /// Refresh access token using refresh token
    /// </summary>
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (string.IsNullOrEmpty(request.AccessToken) || string.IsNullOrEmpty(request.RefreshToken))
            return BadRequest(new { message = "Access token and refresh token are required" });

        var command = new RefreshTokenCommand(request.AccessToken, request.RefreshToken);
        var result = await _authService.RefreshTokenAsync(command);

        if (result == null)
            return Unauthorized(new { message = "Invalid or expired refresh token" });

        return Ok(result);
    }

    /// <summary>
    /// Logout and invalidate refresh token
    /// </summary>
    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        var userId = GetCurrentUserId();
        
        // If user is not authenticated (token expired), just return success
        // The client will clear local storage anyway
        if (userId == null)
            return Ok(new { message = "Logged out successfully" });

        // If authenticated, invalidate the refresh token
        await _authService.LogoutAsync(userId.Value);
        return Ok(new { message = "Logged out successfully" });
    }

    /// <summary>
    /// Get current authenticated user info
    /// </summary>
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = GetCurrentUserId();
        if (userId == null)
            return Unauthorized();

        var user = await _authService.GetCurrentUserAsync(userId.Value);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    /// <summary>
    /// Change password for current user
    /// </summary>
    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userId = GetCurrentUserId();
        if (userId == null)
            return Unauthorized();

        if (string.IsNullOrEmpty(request.CurrentPassword) || string.IsNullOrEmpty(request.NewPassword))
            return BadRequest(new { message = "Current password and new password are required" });

        var command = new AuthChangePasswordCommand(request.CurrentPassword, request.NewPassword);
        var success = await _authService.ChangePasswordAsync(userId.Value, command);

        if (!success)
            return BadRequest(new { message = "Invalid current password" });

        return Ok(new { message = "Password changed successfully" });
    }

    private int? GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst("userId") ?? User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            return null;
        return userId;
    }
}
