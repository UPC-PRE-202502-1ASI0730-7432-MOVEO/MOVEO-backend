using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Model.ValueObjects;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Moveo_backend.UserManagement.Domain.Services;
using Moveo_backend.Rental.Domain.Services;
using Moveo_backend.Rental.Domain.Repositories;
using Moveo_backend.Rental.Infrastructure.Persistence.EFC.Repository;
using Moveo_backend.UserManagement.Domain.Model.Commands;
using Moveo_backend.IAM.Infrastructure.Tokens;
using Moveo_backend.IAM.Infrastructure.Hashing;
using Moveo_backend.IAM.Domain.Services;
using Moveo_backend.IAM.Application.Internal;
using Moveo_backend.Rental.Interfaces.REST.Resources;
using Moveo_backend.Rental.Interfaces.REST.Transform;
using Moveo_backend.Notifications.Domain.Services;
using Moveo_backend.Notifications.Domain.Repositories;
using Moveo_backend.Notifications.Infrastructure.Persistence.EFC.Repository;
using Moveo_backend.Notifications.Interfaces.REST.Resources;
using Moveo_backend.Notifications.Interfaces.REST.Transform;
using Moveo_backend.Notifications.Domain.Model.Commands;
using Moveo_backend.AdventureRoutes.Domain.Services;
using Moveo_backend.AdventureRoutes.Domain.Repositories;
using Moveo_backend.AdventureRoutes.Infrastructure.Persistence.EFC.Repository;
using Moveo_backend.AdventureRoutes.Interfaces.REST.Resources;
using Moveo_backend.AdventureRoutes.Interfaces.REST.Transform;
using Moveo_backend.AdventureRoutes.Domain.Model.Commands;
using Moveo_backend.SupportTickets.Domain.Services;
using Moveo_backend.SupportTickets.Domain.Repositories;
using Moveo_backend.SupportTickets.Infrastructure.Persistence.EFC.Repository;
using Moveo_backend.Payments.Domain.Services;
using Moveo_backend.Payments.Domain.Repositories;
using Moveo_backend.Payments.Infrastructure.Persistence;
using Moveo_backend.Payments.Application;
using Moveo_backend.Reviews.Domain.Services;
using Moveo_backend.Reviews.Domain.Repositories;
using Moveo_backend.Reviews.Infrastructure.Persistence;
using Moveo_backend.Reviews.Application;

var builder = WebApplication.CreateBuilder(args);

// ------------------------- Services & Swagger -------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Moveo Backend API", Version = "v1" });
    
    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ------------------------- JWT Configuration -------------------------
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);

var secretKey = jwtSettings["Secret"] ?? throw new Exception("JWT Secret is not configured");
var key = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// ------------------------- CORS -------------------------
var corsPolicyName = "AllowMoveoFrontend";
if (builder.Environment.IsDevelopment())
{
    // Development: allow any origin to simplify frontend testing
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(corsPolicyName, policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });
}
else
{
    // Production: allow only specific origins (add your frontend URL(s) here)
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(corsPolicyName, policy =>
        {
            policy.WithOrigins(
                    "http://localhost:5173",
                    "https://moveo-backend-production.up.railway.app"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
}

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

// UserManagement Services (for UsersController)
builder.Services.AddScoped<Moveo_backend.UserManagement.Domain.Repositories.IUserRepository, Moveo_backend.UserManagement.Infrastructure.Persistence.EFC.Repositories.UserRepository>();
builder.Services.AddScoped<Moveo_backend.UserManagement.Domain.Services.IUserCommandService, Moveo_backend.UserManagement.Application.CommandServices.UserCommandService>();
builder.Services.AddScoped<Moveo_backend.UserManagement.Domain.Services.IUserQueryService, Moveo_backend.UserManagement.Application.QueryServices.UserQueryService>();

// Notifications Services
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Adventure Routes Services
builder.Services.AddScoped<IAdventureRouteRepository, AdventureRouteRepository>();
builder.Services.AddScoped<IAdventureRouteService, AdventureRouteService>();

// Support Tickets Services
builder.Services.AddScoped<ISupportTicketRepository, SupportTicketRepository>();
builder.Services.AddScoped<ISupportTicketService, SupportTicketService>();

// Payments Services
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// Reviews Services
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();

// IAM Services
builder.Services.AddScoped<IHashingService, BcryptHashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

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
app.UseCors(corsPolicyName);
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers (for AuthController and other future controllers)
app.MapControllers();

// Note: Users endpoints are handled by UsersController
// Note: Rentals endpoints are handled by RentalController

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");
app.Run();
