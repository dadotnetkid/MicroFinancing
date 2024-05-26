using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroFinancing.Entities.Common;

internal static class EFConfigurationExtensions
{
    public static void AddAuditRelationShip<TKey>(EntityTypeBuilder<BaseEntity<TKey>> entity)
    {

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