using Moveo_backend.UserManagement.Domain.Model.Commands;
using Moveo_backend.UserManagement.Domain.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer(); // para minimal APIs
builder.Services.AddSwaggerGen();           // para generar documentación OpenAPI
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      // activa Swagger JSON
    app.UseSwaggerUI();    // activa la interfaz web
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Obtener todos los usuarios
app.MapGet("/api/v1/users", async ([FromServices] IUserService userService) =>
    {
        var users = await userService.GetAllUsersAsync();
        return Results.Ok(users);
    })
    .WithName("GetUsers")
    .WithTags("Users");

// Crear un usuario
app.MapPost("/api/v1/users", async ([FromBody] CreateUserCommand command, [FromServices] IUserService userService) =>
    {
        var createdUser = await userService.CreateUserAsync(command);
        return Results.Created($"/api/v1/users/{createdUser.Id}", createdUser);
    })
    .WithName("CreateUser")
    .WithTags("Users");

// Actualizar un usuario
app.MapPut("/api/v1/users/{id:int}", async (int id, [FromBody] UpdateUserCommand command, [FromServices] IUserService userService) =>
    {
        var updatedUser = await userService.UpdateUserAsync(id, command);
        return updatedUser != null ? Results.Ok(updatedUser) : Results.NotFound();
    })
    .WithName("UpdateUser")
    .WithTags("Users");

// Cambiar contraseña
app.MapPut("/api/v1/users/{id:int}/password", async (int id, [FromBody] ChangePasswordCommand command, [FromServices] IUserService userService) =>
    {
        var success = await userService.ChangePasswordAsync(id, command);
        return success ? Results.NoContent() : Results.NotFound();
    })
    .WithName("ChangeUserPassword")
    .WithTags("Users");

// Cambiar rol
app.MapPut("/api/v1/users/{id:int}/role", async (int id, [FromBody] ChangeUserRoleCommand command, [FromServices] IUserService userService) =>
    {
        var success = await userService.ChangeUserRoleAsync(id, command);
        return success ? Results.NoContent() : Results.NotFound();
    })
    .WithName("ChangeUserRole")
    .WithTags("Users");

// Eliminar usuario
app.MapDelete("/api/v1/users/{id:int}", async (int id, [FromServices] IUserService userService) =>
    {
        var deleted = await userService.DeleteUserAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound();
    })
    .WithName("DeleteUser")
    .WithTags("Users");



app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}