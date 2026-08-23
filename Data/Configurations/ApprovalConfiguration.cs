using CapEx.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapEx.Data.Configurations;

public class ApprovalConfiguration : IEntityTypeConfiguration<Approval>
{
    public void Configure(EntityTypeBuilder<Approval> builder)
    {
        builder.ToTable("Approvals", "dbo");

        builder.HasKey(a => a.ApprovalId);

        builder.Property(a => a.Approved)
            .IsRequired();

        builder.Property(a => a.Comment)
            .HasMaxLength(Approval.MaxCommentLength);

        builder.Property(a => a.CreatedUtc)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.HasOne(a => a.Request)
            .WithMany(r => r.Approvals)
            .HasForeignKey(a => a.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.ActedByUser)
            .WithMany(u => u.Approvals)
            .HasForeignKey(a => a.ActedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => new { a.RequestId, a.ActedByUserId })
            .IsUnique();

        builder.HasIndex(a => a.RequestId);
    }
}
