using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Moveo_backend.UserManagement.Domain.Services;
using Moveo_backend.Rental.Domain.Services;
using Moveo_backend.Rental.Domain.Repositories;
using Moveo_backend.Rental.Infrastructure.Persistence.EFC.Repository;
using Moveo_backend.UserManagement.Domain.Model.Commands;

var builder = WebApplication.CreateBuilder(args);

// ------------------------- Services & Swagger -------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------------- DbContext & MySQL -------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
        throw new Exception("Database connection string is not set.");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
           .LogTo(Console.WriteLine, LogLevel.Information)
           .EnableSensitiveDataLogging()
           .EnableDetailedErrors();
});

// ------------------------- Dependency Injection -------------------------
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IRentalService, RentalService>();

var app = builder.Build();

// ------------------------- Ensure Database Created -------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.EnsureCreated();
        Console.WriteLine("✅ Base de datos y tablas creadas correctamente (si no existían).");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Error al crear la base de datos o las tablas:");
        Console.WriteLine(ex.Message);
        if (ex.InnerException != null)
            Console.WriteLine($"InnerException: {ex.InnerException.Message}");
        throw; // re-lanza para que la app no siga corriendo con fallo crítico
    }
}

// ------------------------- Middleware -------------------------
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// ------------------------- USERS Endpoints -------------------------
app.MapGet("/api/v1/users", async ([FromServices] IUserService userService) =>
{
    var users = await userService.GetAllUsersAsync();
    return Results.Ok(users);
})
.WithName("GetUsers")
.WithTags("Users");

app.MapPost("/api/v1/users", async ([FromBody] CreateUserCommand command, [FromServices] IUserService userService) =>
{
    var createdUser = await userService.CreateUserAsync(command);
    return Results.Created($"/api/v1/users/{createdUser.Id}", createdUser);
})
.WithName("CreateUser")
.WithTags("Users");

app.MapPut("/api/v1/users/{id:int}", async (int id, [FromBody] UpdateUserCommand command, [FromServices] IUserService userService) =>
{
    var updatedUser = await userService.UpdateUserAsync(id, command);
    return updatedUser != null ? Results.Ok(updatedUser) : Results.NotFound();
})
.WithName("UpdateUser")
.WithTags("Users");

app.MapPut("/api/v1/users/{id:int}/password", async (int id, [FromBody] ChangePasswordCommand command, [FromServices] IUserService userService) =>
{
    var success = await userService.ChangePasswordAsync(id, command);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("ChangeUserPassword")
.WithTags("Users");

app.MapPut("/api/v1/users/{id:int}/role", async (int id, [FromBody] ChangeUserRoleCommand command, [FromServices] IUserService userService) =>
{
    var success = await userService.ChangeUserRoleAsync(id, command);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("ChangeUserRole")
.WithTags("Users");

app.MapDelete("/api/v1/users/{id:int}", async (int id, [FromServices] IUserService userService) =>
{
    var deleted = await userService.DeleteUserAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteUser")
.WithTags("Users");

// ------------------------- VEHICLES Endpoints -------------------------
app.MapGet("/api/v1/vehicles", async ([FromServices] IVehicleService vehicleService) =>
{
    var vehicles = await vehicleService.GetAllAsync();
    return Results.Ok(vehicles);
})
.WithName("GetVehicles")
.WithTags("Vehicles");

app.MapPost("/api/v1/vehicles", async ([FromBody] CreateVehicleCommand command, [FromServices] IVehicleService vehicleService) =>
{
    var vehicle = await vehicleService.CreateVehicleAsync(command);
    return Results.Created($"/api/v1/vehicles/{vehicle.Id}", vehicle);
})
.WithName("CreateVehicle")
.WithTags("Vehicles");

app.MapPut("/api/v1/vehicles/{id:guid}", async (Guid id, [FromBody] UpdateVehicleCommand command, [FromServices] IVehicleService vehicleService) =>
{
    var updatedCommand = command with { Id = id };
    var updated = await vehicleService.UpdateVehicleAsync(updatedCommand);
    return updated != null ? Results.Ok(updated) : Results.NotFound();
})
.WithName("UpdateVehicle")
.WithTags("Vehicles");

app.MapDelete("/api/v1/vehicles/{id:guid}", async (Guid id, [FromServices] IVehicleService vehicleService) =>
{
    var deleted = await vehicleService.DeleteVehicleAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteVehicle")
.WithTags("Vehicles");

// ------------------------- RENTALS Endpoints -------------------------
app.MapGet("/api/v1/rentals", async ([FromServices] IRentalService rentalService) =>
{
    var rentals = await rentalService.GetAllAsync();
    return Results.Ok(rentals);
})
.WithName("GetRentals")
.WithTags("Rentals");

app.MapPost("/api/v1/rentals", async ([FromBody] CreateRentalCommand command, [FromServices] IRentalService rentalService) =>
{
    var rental = await rentalService.CreateRentalAsync(command);
    return Results.Created($"/api/v1/rentals/{rental.Id}", rental);
})
.WithName("CreateRental")
.WithTags("Rentals");

app.MapPut("/api/v1/rentals/{id:guid}", async (Guid id, [FromBody] UpdateRentalCommand command, [FromServices] IRentalService rentalService) =>
{
    var updatedCommand = command with { Id = id };
    var updated = await rentalService.UpdateRentalAsync(updatedCommand);
    return updated != null ? Results.Ok(updated) : Results.NotFound();
})
.WithName("UpdateRental")
.WithTags("Rentals");

app.MapPut("/api/v1/rentals/{id:guid}/cancel", async (Guid id, [FromBody] CancelRentalCommand command, [FromServices] IRentalService rentalService) =>
{
    var updatedCommand = command with { Id = id };
    var success = await rentalService.CancelRentalAsync(updatedCommand);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("CancelRental")
.WithTags("Rentals");

app.MapPut("/api/v1/rentals/{id:guid}/finish", async (Guid id, [FromBody] FinishRentalCommand command, [FromServices] IRentalService rentalService) =>
{
    var updatedCommand = command with { Id = id };
    var success = await rentalService.FinishRentalAsync(updatedCommand);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("FinishRental")
.WithTags("Rentals");

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");
app.Run();
