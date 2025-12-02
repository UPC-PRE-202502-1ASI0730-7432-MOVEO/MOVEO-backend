using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moveo_backend.Rental.Domain.Model.Aggregates;

namespace Moveo_backend.Rental.Infrastructure.Persistence.EFC.Configuration;

public class RentalEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Model.Aggregates.Rental>
{
    public void Configure(EntityTypeBuilder<Domain.Model.Aggregates.Rental> builder)
    {
        builder.ToTable("rentals");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.VehicleId)
            .HasColumnName("vehicle_id")
            .IsRequired();

        builder.Property(r => r.RenterId)
            .HasColumnName("renter_id")
            .IsRequired();

        builder.Property(r => r.OwnerId)
            .HasColumnName("owner_id")
            .IsRequired();

        builder.Property(r => r.Status)
            .HasColumnName("status")
            .HasDefaultValue("pending");

        builder.Property(r => r.Notes)
            .HasColumnName("notes");

        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(r => r.AcceptedAt)
            .HasColumnName("accepted_at");

        // ✅ Propiedad de valor: RentalPeriod
        builder.OwnsOne(r => r.RentalPeriod, rentalPeriod =>
        {
            rentalPeriod.Property(rp => rp.StartDate)
                .HasColumnName("start_date")
                .IsRequired();

            rentalPeriod.Property(rp => rp.EndDate)
                .HasColumnName("end_date")
                .IsRequired();
        });

        // ✅ Propiedad de valor: TotalPrice
        builder.OwnsOne(r => r.TotalPrice, totalPrice =>
        {
            totalPrice.Property(tp => tp.Amount)
                .HasColumnName("total_price")
                .IsRequired();

            totalPrice.Property(tp => tp.Currency)
                .HasColumnName("currency")
                .HasDefaultValue("USD");
        });

        // ✅ Propiedad de valor: PickupLocation
        builder.OwnsOne(r => r.PickupLocation, pickup =>
        {
            pickup.Property(l => l.District)
                .HasColumnName("pickup_district");

            pickup.Property(l => l.Address)
                .HasColumnName("pickup_address")
                .IsRequired();

            pickup.Property(l => l.Lat)
                .HasColumnName("pickup_lat");

            pickup.Property(l => l.Lng)
                .HasColumnName("pickup_lng");
        });

        // ✅ Propiedad de valor: ReturnLocation
        builder.OwnsOne(r => r.ReturnLocation, ret =>
        {
            ret.Property(l => l.District)
                .HasColumnName("return_district");

            ret.Property(l => l.Address)
                .HasColumnName("return_address")
                .IsRequired();

            ret.Property(l => l.Lat)
                .HasColumnName("return_lat");

            ret.Property(l => l.Lng)
                .HasColumnName("return_lng");
        });
    }
}
