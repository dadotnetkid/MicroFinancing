using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroFinancing.Entities.Configurations;

public sealed class LendingForApprovalConfiguration : IEntityTypeConfiguration<LendingForApproval>
{
    public void Configure(EntityTypeBuilder<LendingForApproval> entity)
    {
        entity.HasOne(x => x.CollectorUser).WithMany().IsRequired().HasForeignKey(x => x.Collector)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(c => c.Customers)
              .WithMany()
              .HasForeignKey(c => c.CustomerId)
              .IsRequired(true)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(c => new { c.IsDeleted, c.CustomerId, c.Collector, c.IsActive }).IsUnique(false);

        entity.HasQueryFilter(c => !c.IsDeleted);

        entity.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE");

        entity.HasOne(c => c.Creator).WithMany()
              .IsRequired(false)
              .HasForeignKey(c => c.CreatorUserId)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(c => c.LastModifier).WithMany()
              .IsRequired(false)
              .HasForeignKey(c => c.LastModifierUserId)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(c => c.DeleterUser).WithMany()
              .IsRequired(false)
              .HasForeignKey(c => c.DeleterUserId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}
