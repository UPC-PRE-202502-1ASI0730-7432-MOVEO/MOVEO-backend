using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Model.ValueObjects;
using Moveo_backend.UserManagement.Domain.Model.Aggregates;
using Moveo_backend.Notifications.Domain.Model.Aggregates;
using Moveo_backend.AdventureRoutes.Domain.Model.Aggregates;
using Moveo_backend.SupportTickets.Domain.Model.Aggregates;
using Moveo_backend.Payments.Domain.Model.Aggregates;
using Moveo_backend.Reviews.Domain.Model.Aggregates;

namespace Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    public DbSet<Rental.Domain.Model.Aggregates.Rental> Rentals { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<AdventureRoute> AdventureRoutes { get; set; } = null!;
    public DbSet<SupportTicket> SupportTickets { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // -------------------- USER --------------------
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);
            
            // Map Value Objects as owned types
            entity.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.FirstName).HasColumnName("FirstName").IsRequired();
                name.Property(n => n.LastName).HasColumnName("LastName").IsRequired();
            });
            
            entity.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Address).HasColumnName("Email").IsRequired();
            });
            
            entity.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.Value).HasColumnName("Password").IsRequired();
            });
            
            entity.OwnsOne(u => u.Role, role =>
            {
                role.Property(r => r.Value).HasColumnName("Role").IsRequired();
            });
            
            entity.OwnsOne(u => u.Preferences, prefs =>
            {
                prefs.Property(p => p.Language).HasColumnName("Language");
                prefs.Property(p => p.EmailNotifications).HasColumnName("EmailNotifications");
                prefs.Property(p => p.PushNotifications).HasColumnName("PushNotifications");
                prefs.Property(p => p.SmsNotifications).HasColumnName("SmsNotifications");
                prefs.Property(p => p.AutoAcceptRentals).HasColumnName("AutoAcceptRentals");
                prefs.Property(p => p.MinimumRentalDays).HasColumnName("MinimumRentalDays");
                prefs.Property(p => p.InstantBooking).HasColumnName("InstantBooking");
            });
            
            entity.Property(u => u.Phone).IsRequired();
            entity.Property(u => u.Dni).IsRequired();
            entity.Property(u => u.LicenseNumber).IsRequired();
            entity.Property(u => u.Address).IsRequired();
            entity.Property(u => u.RefreshToken).IsRequired(false);
            entity.Property(u => u.RefreshTokenExpiryTime).IsRequired(false);
            entity.Property(u => u.CreatedAt).IsRequired();
            entity.Property(u => u.UpdatedAt).IsRequired(false);
        });

        // -------------------- VEHICLE --------------------
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Brand).IsRequired();
            entity.Property(v => v.Model).IsRequired();
            entity.Property(v => v.Transmission).IsRequired();
            entity.Property(v => v.FuelType).IsRequired();
            entity.Property(v => v.Status).IsRequired();

            // Owned types para Money
            entity.OwnsOne(v => v.DailyPrice, dp =>
            {
                dp.Property(p => p.Amount).HasColumnName("DailyPrice").HasColumnType("decimal(18,2)");
                dp.Property(p => p.Currency).HasColumnName("DailyPriceCurrency");
            });
            entity.OwnsOne(v => v.DepositAmount, da =>
            {
                da.Property(p => p.Amount).HasColumnName("DepositAmount").HasColumnType("decimal(18,2)");
                da.Property(p => p.Currency).HasColumnName("DepositAmountCurrency");
            });

            // Owned type para Location
            entity.OwnsOne(v => v.Location, loc =>
            {
                loc.Property(l => l.District).HasColumnName("LocationDistrict");
                loc.Property(l => l.Address).HasColumnName("LocationAddress");
                loc.Property(l => l.Lat).HasColumnName("LocationLat");
                loc.Property(l => l.Lng).HasColumnName("LocationLng");
            });

            // Listas como JSON
            entity.Property(v => v.FeaturesJson).HasColumnType("json");
            entity.Property(v => v.RestrictionsJson).HasColumnType("json");
            entity.Property(v => v.PhotosJson).HasColumnType("json");
        });

        // -------------------- RENTAL --------------------
        modelBuilder.Entity<Rental.Domain.Model.Aggregates.Rental>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Status).IsRequired();

            // Owned types para Money
            entity.OwnsOne(r => r.TotalPrice, tp =>
            {
                tp.Property(p => p.Amount).HasColumnName("TotalPrice").HasColumnType("decimal(18,2)");
                tp.Property(p => p.Currency).HasColumnName("TotalPriceCurrency");
            });

            // Owned types para Location
            entity.OwnsOne(r => r.PickupLocation, loc =>
            {
                loc.Property(l => l.District).HasColumnName("PickupLocationDistrict");
                loc.Property(l => l.Address).HasColumnName("PickupLocationAddress");
                loc.Property(l => l.Lat).HasColumnName("PickupLocationLat");
                loc.Property(l => l.Lng).HasColumnName("PickupLocationLng");
            });
            entity.OwnsOne(r => r.ReturnLocation, loc =>
            {
                loc.Property(l => l.District).HasColumnName("ReturnLocationDistrict");
                loc.Property(l => l.Address).HasColumnName("ReturnLocationAddress");
                loc.Property(l => l.Lat).HasColumnName("ReturnLocationLat");
                loc.Property(l => l.Lng).HasColumnName("ReturnLocationLng");
            });

            // Owned type para DateRange (si lo usas en Rental)
            entity.OwnsOne(r => r.RentalPeriod);
        });

        // -------------------- NOTIFICATION --------------------
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(n => n.Id);
            entity.Property(n => n.UserId).IsRequired();
            entity.Property(n => n.Type).IsRequired();
            entity.Property(n => n.Title).IsRequired();
            entity.Property(n => n.Body).IsRequired();
            entity.Property(n => n.Read).IsRequired();
            entity.Property(n => n.CreatedAt).IsRequired();
            entity.Property(n => n.RelatedId).IsRequired(false);
            entity.Property(n => n.RelatedType).IsRequired(false);
        });

        // -------------------- ADVENTURE ROUTE --------------------
        modelBuilder.Entity<AdventureRoute>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Name).IsRequired();
            entity.Property(r => r.Description).IsRequired();
            entity.Property(r => r.Difficulty).IsRequired();
            entity.Property(r => r.Distance).IsRequired();
            entity.Property(r => r.EstimatedTime).IsRequired();
            entity.Property(r => r.WaypointsJson).HasColumnType("json");
            entity.Property(r => r.ImagesJson).HasColumnType("json");
            entity.Property(r => r.CreatedAt).IsRequired();
            entity.Property(r => r.IsActive).IsRequired();
        });

        // -------------------- SUPPORT TICKET --------------------
        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.UserId).IsRequired();
            entity.Property(t => t.Subject).IsRequired();
            entity.Property(t => t.Description).IsRequired();
            entity.Property(t => t.Category).IsRequired();
            entity.Property(t => t.Priority).IsRequired();
            entity.Property(t => t.Status).IsRequired();
            entity.Property(t => t.CreatedAt).IsRequired();
        });

        // -------------------- PAYMENT --------------------
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.RentalId).IsRequired();
            entity.Property(p => p.PayerId).IsRequired();
            entity.Property(p => p.RecipientId).IsRequired();
            entity.Property(p => p.Amount).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(p => p.Currency).IsRequired();
            entity.Property(p => p.Status).IsRequired();
            entity.Property(p => p.PaymentMethod).IsRequired();
            entity.Property(p => p.TransactionId).IsRequired(false);
            entity.Property(p => p.Description).IsRequired(false);
            entity.Property(p => p.CreatedAt).IsRequired();
            entity.Property(p => p.PaidAt).IsRequired(false);
            entity.Property(p => p.FailedAt).IsRequired(false);
            entity.Property(p => p.FailureReason).IsRequired(false);
        });

        // -------------------- REVIEW --------------------
        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.VehicleId).IsRequired();
            entity.Property(r => r.RentalId).IsRequired();
            entity.Property(r => r.ReviewerId).IsRequired();
            entity.Property(r => r.OwnerId).IsRequired();
            entity.Property(r => r.Rating).IsRequired();
            entity.Property(r => r.Comment).IsRequired(false);
            entity.Property(r => r.CreatedAt).IsRequired();
            entity.Property(r => r.UpdatedAt).IsRequired(false);
        });
    }
}