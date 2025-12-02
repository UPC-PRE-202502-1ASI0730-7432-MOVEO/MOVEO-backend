using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Moveo_backend.Shared.Domain.Repositories;
using Moveo_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
// IAM
using Moveo_backend.IAM.Domain.Services;
using Moveo_backend.IAM.Application.Internal;
using Moveo_backend.IAM.Infrastructure.Hashing;
using Moveo_backend.IAM.Infrastructure.Tokens;
// UserManagement
using Moveo_backend.UserManagement.Domain.Repositories;
using Moveo_backend.UserManagement.Domain.Services;
using Moveo_backend.UserManagement.Application.CommandServices;
using Moveo_backend.UserManagement.Application.QueryServices;
using Moveo_backend.UserManagement.Infrastructure.Persistence.EFC.Repositories;
// Rental
using Moveo_backend.Rental.Domain.Repositories;
using Moveo_backend.Rental.Domain.Services;
using Moveo_backend.Rental.Infrastructure.Persistence.EFC.Repository;
using Moveo_backend.Rental.Infrastructure.Persistence.EFC.Repositories;
using Moveo_backend.Rental.Application.CommandServices;
using Moveo_backend.Rental.Application.QueryServices;
// Adventure
using Moveo_backend.Adventure.Domain.Repositories;
using Moveo_backend.Adventure.Domain.Services;
using Moveo_backend.Adventure.Application.Internal.CommandServices;
using Moveo_backend.Adventure.Application.Internal.QueryServices;
using Moveo_backend.Adventure.Infrastructure.Persistence.EFC.Repositories;
// Payment
using Moveo_backend.Payment.Domain.Repositories;
using Moveo_backend.Payment.Domain.Services;
using Moveo_backend.Payment.Application.Internal.CommandServices;
using Moveo_backend.Payment.Application.Internal.QueryServices;
using Moveo_backend.Payment.Infrastructure.Persistence.EFC.Repositories;
// Notification
using Moveo_backend.Notification.Domain.Repositories;
using Moveo_backend.Notification.Domain.Services;
using Moveo_backend.Notification.Application.Internal.CommandServices;
using Moveo_backend.Notification.Application.Internal.QueryServices;
using Moveo_backend.Notification.Infrastructure.Persistence.EFC.Repositories;
// Support
using Moveo_backend.Support.Domain.Repositories;
using Moveo_backend.Support.Domain.Services;
using Moveo_backend.Support.Application.Internal.CommandServices;
using Moveo_backend.Support.Application.Internal.QueryServices;
using Moveo_backend.Support.Infrastructure.Persistence.EFC.Repositories;
// UserReview
using Moveo_backend.UserReview.Domain.Repositories;
using Moveo_backend.UserReview.Domain.Services;
using Moveo_backend.UserReview.Application.Internal.CommandServices;
using Moveo_backend.UserReview.Application.Internal.QueryServices;
using Moveo_backend.UserReview.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ------------------------- Services & Swagger -------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MOVEO Backend API",
        Version = "v1",
        Description = "API para la plataforma de alquiler de vehículos MOVEO"
    });

    // Configurar JWT en Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa tu token JWT. Ejemplo: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ------------------------- CORS -------------------------
var corsPolicyName = "AllowMoveoFrontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy.WithOrigins(
                  "https://moveo-frontend-0sbk.onrender.com",
                  "http://localhost:5173",
                  "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ------------------------- DbContext & MySQL -------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
        throw new InvalidOperationException("Database connection string is not set.");
    
    // Usar versión fija en lugar de AutoDetect para evitar errores de conexión al iniciar
    options.UseMySql(connectionString, 
                     new MySqlServerVersion(new Version(8, 0, 21)),
                     mySqlOptions =>
                     {
                         // Reintentar conexión si falla (útil en Railway)
                         mySqlOptions.EnableRetryOnFailure(
                             maxRetryCount: 5,
                             maxRetryDelay: TimeSpan.FromSeconds(10),
                             errorNumbersToAdd: null);
                     })
           .LogTo(Console.WriteLine, LogLevel.Information)
           .EnableSensitiveDataLogging()
           .EnableDetailedErrors();
});

// ------------------------- Shared Dependencies -------------------------
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ------------------------- IAM / JWT Authentication -------------------------
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IHashingService, BcryptHashingService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// JWT Authentication
var jwtSecret = builder.Configuration["JwtSettings:Secret"];
var jwtIssuer = builder.Configuration["JwtSettings:Issuer"];
var jwtAudience = builder.Configuration["JwtSettings:Audience"];

if (!string.IsNullOrEmpty(jwtSecret) && !string.IsNullOrEmpty(jwtIssuer) && !string.IsNullOrEmpty(jwtAudience))
{
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
    builder.Services.AddAuthorization();
}
else
{
    Console.WriteLine("⚠️ JWT Settings no configurados correctamente");
}

// ------------------------- UserManagement Dependencies -------------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

// ------------------------- Rental Dependencies -------------------------
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewCommandService, ReviewCommandService>();
builder.Services.AddScoped<IReviewQueryService, ReviewQueryService>();

// ------------------------- Adventure Dependencies -------------------------
builder.Services.AddScoped<IAdventureRouteRepository, AdventureRouteRepository>();
builder.Services.AddScoped<IAdventureRouteCommandService, AdventureRouteCommandService>();
builder.Services.AddScoped<IAdventureRouteQueryService, AdventureRouteQueryService>();

// ------------------------- Payment Dependencies -------------------------
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentCommandService, PaymentCommandService>();
builder.Services.AddScoped<IPaymentQueryService, PaymentQueryService>();

// ------------------------- Notification Dependencies -------------------------
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationCommandService, NotificationCommandService>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();

// ------------------------- Support Dependencies -------------------------
builder.Services.AddScoped<ISupportTicketRepository, SupportTicketRepository>();
builder.Services.AddScoped<ITicketMessageRepository, TicketMessageRepository>();
builder.Services.AddScoped<ISupportTicketCommandService, SupportTicketCommandService>();
builder.Services.AddScoped<ISupportTicketQueryService, SupportTicketQueryService>();
builder.Services.AddScoped<ITicketMessageCommandService, TicketMessageCommandService>();
builder.Services.AddScoped<ITicketMessageQueryService, TicketMessageQueryService>();

// ------------------------- UserReview Dependencies -------------------------
builder.Services.AddScoped<IUserReviewRepository, UserReviewRepository>();
builder.Services.AddScoped<IUserReviewCommandService, UserReviewCommandService>();
builder.Services.AddScoped<IUserReviewQueryService, UserReviewQueryService>();

var app = builder.Build();

app.UseDeveloperExceptionPage();

// ------------------------- Ensure Database Created -------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        await db.Database.EnsureCreatedAsync();
        Console.WriteLine("✅ Base de datos y tablas creadas correctamente (si no existían).");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Error al crear la base de datos o las tablas:");
        Console.WriteLine(ex.Message);
        if (ex.InnerException != null)
            Console.WriteLine($"InnerException: {ex.InnerException.Message}");
        throw;
    }
}

// ------------------------- Middleware -------------------------
app.UseCors("AllowMoveoFrontend");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");
await app.RunAsync();
