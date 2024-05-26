using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroFinancing.Entities.Configurations;

public sealed class TermConfiguration : IEntityTypeConfiguration<Term>
{
    public void Configure(EntityTypeBuilder<Term> builder)
    {
        builder.HasOne(c => c.Creator).WithMany()
            .IsRequired(false)
            .HasForeignKey(c => c.CreatorUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.LastModifier).WithMany()
            .IsRequired(false)
            .HasForeignKey(c => c.LastModifierUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.DeleterUser).WithMany()
            .IsRequired(false)
            .HasForeignKey(c => c.DeleterUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}