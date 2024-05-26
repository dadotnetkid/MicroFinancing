using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroFinancing.Entities.Configurations;

public class BatchInCustomersConfiguration : IEntityTypeConfiguration<BatchInCustomer>
{
    public void Configure(EntityTypeBuilder<BatchInCustomer> entity)
    {
        entity.HasOne(c => c.Batch)
            .WithMany(c => c.Customers)
            .HasForeignKey(c => c.BatchId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(c => c.Customers)
            .WithMany(c => c.Batch)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

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