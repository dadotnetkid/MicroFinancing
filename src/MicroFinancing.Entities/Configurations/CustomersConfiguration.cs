using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroFinancing.Entities.Configurations;

public sealed class CustomersConfiguration : IEntityTypeConfiguration<Customers>
{
    public void Configure(EntityTypeBuilder<Customers> entity)
    {
        entity.HasMany(x => x.Lending).WithOne(x => x.Customers).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
        entity.HasMany(x => x.Payments).WithOne(x => x.Customers).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
        entity.Property(e => e.FullName).HasComputedColumnSql("FirstName + ' ' + LastName ");

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