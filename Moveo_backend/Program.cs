using Moveo_backend.UserManagement.Domain.Model.Commands;
using Moveo_backend.UserManagement.Domain.Services;
using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Services;
using Moveo_backend.Rental.Domain.Repositories;
using Moveo_backend.Rental.Domain.Model.Aggregates;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer(); // para minimal APIs
builder.Services.AddSwaggerGen();           // para generar documentación OpenAPI
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IVehicleService, VehicleService>();
builder.Services.AddSingleton<IRentalService, RentalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      // activa Swagger JSON
    app.UseSwaggerUI();    // activa la interfaz web
}

app.UseHttpsRedirection();

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

// -------------------- VEHICLES --------------------
// Obtener todos los vehículos
app.MapGet("/api/v1/vehicles", async ([FromServices] IVehicleService vehicleService) =>
{
    var vehicles = await vehicleService.GetAllAsync();
    return Results.Ok(vehicles);
})
.WithName("GetVehicles")
.WithTags("Vehicles");

// Crear un vehículo
app.MapPost("/api/v1/vehicles", async ([FromBody] CreateVehicleCommand command, [FromServices] IVehicleService vehicleService) =>
{
    var vehicle = await vehicleService.CreateVehicleAsync(command);
    return Results.Created($"/api/v1/vehicles/{vehicle.Id}", vehicle);
})
.WithName("CreateVehicle")
.WithTags("Vehicles");

// Actualizar vehículo
app.MapPut("/api/v1/vehicles/{id:guid}", async (Guid id, [FromBody] UpdateVehicleCommand command, [FromServices] IVehicleService vehicleService) =>
{
    var updatedCommand = command with { Id = id };
    var updated = await vehicleService.UpdateVehicleAsync(updatedCommand);
    return updated != null ? Results.Ok(updated) : Results.NotFound();
})
.WithName("UpdateVehicle")
.WithTags("Vehicles");

// Eliminar vehículo
app.MapDelete("/api/v1/vehicles/{id:guid}", async (Guid id, [FromServices] IVehicleService vehicleService) =>
{
    var deleted = await vehicleService.DeleteVehicleAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteVehicle")
.WithTags("Vehicles");

// -------------------- RENTALS --------------------
// Obtener todos los rentals
app.MapGet("/api/v1/rentals", async ([FromServices] IRentalService rentalService) =>
{
    var rentals = await rentalService.GetAllAsync();
    return Results.Ok(rentals);
})
.WithName("GetRentals")
.WithTags("Rentals");

// Crear rental
app.MapPost("/api/v1/rentals", async ([FromBody] CreateRentalCommand command, [FromServices] IRentalService rentalService) =>
{
    var rental = await rentalService.CreateRentalAsync(command);
    return Results.Created($"/api/v1/rentals/{rental.Id}", rental);
})
.WithName("CreateRental")
.WithTags("Rentals");

// Actualizar rental
app.MapPut("/api/v1/rentals/{id:guid}", async (Guid id, [FromBody] UpdateRentalCommand command, [FromServices] IRentalService rentalService) =>
{
    var updatedCommand = command with { Id = id }; // aseguramos que el comando tenga el Id de la URL
    var updated = await rentalService.UpdateRentalAsync(updatedCommand);
    return updated != null ? Results.Ok(updated) : Results.NotFound();
})
.WithName("UpdateRental")
.WithTags("Rentals");

// Cancelar rental
app.MapPut("/api/v1/rentals/{id:guid}/cancel", async (Guid id, [FromBody] CancelRentalCommand command, [FromServices] IRentalService rentalService) =>
{
    var updatedCommand = command with { Id = id }; // aseguramos que el comando tenga el Id de la URL
    var success = await rentalService.CancelRentalAsync(updatedCommand);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("CancelRental")
.WithTags("Rentals");

// Terminar rental
app.MapPut("/api/v1/rentals/{id:guid}/finish", async (Guid id, [FromBody] FinishRentalCommand command, [FromServices] IRentalService rentalService) =>
{
    var updatedCommand = command with { Id = id };
    var success = await rentalService.FinishRentalAsync(updatedCommand);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("FinishRental")
.WithTags("Rentals");

app.Run();
