using CapEx.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapEx.Data.Configurations;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.ToTable("Requests", "dbo");

        builder.HasKey(r => r.RequestId);

        builder.Property(r => r.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(r => r.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(r => r.Motivation)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(r => r.CreatedUtc)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.Property(r => r.Status)
            .HasColumnType("tinyint")
            .IsRequired();

        builder.HasOne(r => r.RequestedByUser)
            .WithMany(u => u.Requests)
            .HasForeignKey(r => r.RequestedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(r => r.RequestedByUserId);
        builder.HasIndex(r => r.Status);
    }
}
