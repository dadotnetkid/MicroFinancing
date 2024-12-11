using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroFinancing.Entities.Configurations;

public class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Token).IsRequired();
        builder.Property(x => x.Expires).IsRequired();
        builder.HasOne(c => c.User)
               .WithMany(c => c.RefreshToken)
               .IsRequired()
               .HasForeignKey(c=>c.UserId);


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
