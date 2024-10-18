using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroFinancing.Entities.Configurations;

/*
public sealed class InterestRateConfiguration : IEntityTypeConfiguration<InterestRate>
{
    public void Configure(EntityTypeBuilder<InterestRate> builder)
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

        builder.HasData(new[]
        {
            new InterestRate
            {
                Rate = 13.3M,
                Term = 40,
                CreatedAt= DateTime.Now,
            },
            new InterestRate
            {
                Rate = 10M,
                Term = 30,
                CreatedAt= DateTime.Now,
            },
        });
    }
}
*/
