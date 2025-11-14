using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moveo_backend.UserManagement.Domain.Model.Aggregates;


namespace Moveo_backend.UserManagement.Infrastructure.Persistence.EFC.Configuration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        // Propiedades de valor
        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.FirstName)
                .HasColumnName("first_name")
                .IsRequired();

            name.Property(n => n.LastName)
                .HasColumnName("last_name")
                .IsRequired();
        });

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Address)
                .HasColumnName("email")
                .IsRequired();
        });

        builder.OwnsOne(u => u.Password, password =>
        {
            password.Property(p => p.Value)
                .HasColumnName("password")
                .IsRequired();
        });

        builder.OwnsOne(u => u.Role, role =>
        {
            role.Property(r => r.Value)
                .HasColumnName("role")
                .IsRequired();
        });
    }
}