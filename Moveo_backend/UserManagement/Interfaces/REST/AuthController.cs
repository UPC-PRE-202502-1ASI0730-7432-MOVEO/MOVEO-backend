using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Moveo_backend.UserManagement.Domain.Model.Queries;
using Moveo_backend.UserManagement.Domain.Services;
using Moveo_backend.UserManagement.Interfaces.REST.Resources;
using Moveo_backend.UserManagement.Interfaces.REST.Transform;

namespace Moveo_backend.UserManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/auth")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Authentication Endpoints")]
public class AuthController(IUserQueryService userQueryService) : ControllerBase
{
    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Login",
        Description = "Authenticate user with email and password",
        OperationId = "Login"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Login successful", typeof(LoginResponseResource))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid credentials")]
    public async Task<IActionResult> Login([FromBody] LoginResource resource)
    {
        // Buscar usuario por email
        var getUserByEmailQuery = new GetUserByEmailQuery(resource.Email);
        var user = await userQueryService.Handle(getUserByEmailQuery);
        
        if (user is null)
        {
            return Unauthorized(new { message = "Usuario no encontrado" });
        }
        
        // Verificar password (comparación simple, en producción usar hash)
        // DEBUG: Log para ver las contraseñas
        Console.WriteLine($"[DEBUG] Password en BD: '{user.PasswordHash}'");
        Console.WriteLine($"[DEBUG] Password recibida: '{resource.Password}'");
        Console.WriteLine($"[DEBUG] Son iguales: {user.PasswordHash == resource.Password}");
        
        if (user.PasswordHash != resource.Password)
        {
            return Unauthorized(new { message = $"Contraseña incorrecta" });
        }
        
        // Crear respuesta con datos del usuario
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        
        var response = new LoginResponseResource
        {
            Id = userResource.Id,
            Role = userResource.Role,
            FirstName = userResource.FirstName,
            LastName = userResource.LastName,
            Email = userResource.Email,
            Phone = userResource.Phone,
            Dni = userResource.Dni,
            LicenseNumber = userResource.LicenseNumber,
            Avatar = userResource.Avatar,
            Verified = userResource.Verified,
            Stats = userResource.Stats,
            Preferences = userResource.Preferences,
            BankAccount = userResource.BankAccount,
            CreatedAt = userResource.CreatedAt,
            UpdatedAt = userResource.UpdatedAt,
            Message = "Login successful"
        };
        
        return Ok(response);
    }
}
