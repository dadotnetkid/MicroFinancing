using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroFinancing.Entities.Configurations;

public sealed class ItemsConfiguration : IEntityTypeConfiguration<Items>
{
    public void Configure(EntityTypeBuilder<Items> entity)
    {
        entity.HasIndex(c => new { c.IsDeleted }).IsUnique(false);
        entity.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE");

        entity.HasQueryFilter(c => !c.IsDeleted);

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