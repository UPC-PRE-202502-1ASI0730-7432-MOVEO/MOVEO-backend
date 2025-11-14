using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Model.ValueObjects;
using Moveo_backend.UserManagement.Interfaces.REST.Resources;

namespace Moveo_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<UserResource> Users { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    public DbSet<Rental.Domain.Model.Aggregates.Rental> Rentals { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // -------------------- USER --------------------
        modelBuilder.Entity<UserResource>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Email).IsRequired();

            // Owned type para UserPreferences
            entity.OwnsOne(u => u.Preferences, up =>
            {
                up.Property(p => p.Language).HasColumnName("Language").IsRequired();
                up.Property(p => p.EmailNotifications).HasColumnName("EmailNotifications").IsRequired();
                up.Property(p => p.PushNotifications).HasColumnName("PushNotifications").IsRequired();
                up.Property(p => p.SmsNotifications).HasColumnName("SmsNotifications").IsRequired();
                up.Property(p => p.AutoAcceptRentals).HasColumnName("AutoAcceptRentals").IsRequired();
                up.Property(p => p.MinimumRentalDays).HasColumnName("MinimumRentalDays").IsRequired();
                up.Property(p => p.InstantBooking).HasColumnName("InstantBooking").IsRequired();
            });
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
            entity.OwnsOne(v => v.Location);

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
            entity.OwnsOne(r => r.PickupLocation);
            entity.OwnsOne(r => r.ReturnLocation);

            // Owned type para DateRange (si lo usas en Rental)
            entity.OwnsOne(r => r.RentalPeriod);
        });
    }
}