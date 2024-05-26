using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroFinancing.Entities.Configurations;

public sealed class LendingConfiguration : IEntityTypeConfiguration<Lending>
{
    public void Configure(EntityTypeBuilder<Lending> entity)
    {
        entity.HasOne(x => x.CollectorUser).WithMany().IsRequired().HasForeignKey(x => x.Collector)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(x => x.Payments).WithOne(x => x.Lending).HasForeignKey(x => x.LendingId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(c => new { c.IsDeleted, c.CustomerId, c.Collector }).IsUnique(false);

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