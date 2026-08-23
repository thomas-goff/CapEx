using CapEx.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapEx.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "dbo");

        builder.HasKey(u => u.UserId);

        builder.Property(u => u.UserName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(u => u.Password)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(u => u.Role)
            .HasColumnType("tinyint")
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}
