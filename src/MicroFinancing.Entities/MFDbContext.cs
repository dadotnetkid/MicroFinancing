using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MicroFinancing.Entities
{
    public class MFDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaims, ApplicationUserRole, ApplicationUserLogin
        , ApplicationRoleClaims, ApplicationUserToken>
    {
        private readonly DbContextOptions<MFDbContext> _options;
        public MFDbContext(DbContextOptions<MFDbContext> options)
            : base(options)
        {
            _options = options;
        }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Lending> Lendings { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Lending>(entity =>
            {
                entity.HasOne(x => x.CollectorUser).WithMany().IsRequired().HasForeignKey(x => x.Collector)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(x => x.Payments).WithOne(x => x.Lending).HasForeignKey(x => x.LendingId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.Interest)
                    .HasComputedColumnSql("(amount+ItemAmount)*(InterestRate/100)");
                entity.Property(e => e.TotalCredit)
                    .HasComputedColumnSql("((amount+ItemAmount)*(InterestRate/100)) + amount +ItemAmount");

            });
            builder.Entity<Customers>(entity =>
            {
                entity.HasMany(x => x.Lending).WithOne(x => x.Customers).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(x => x.Payments).WithOne(x => x.Customers).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.FullName).HasComputedColumnSql("FirstName + ' ' + LastName ");
            });
            builder.Entity<ApplicationRoleClaims>(entity =>
            {
                entity.ToTable("AspNetRoleClaims");
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId);
                entity.HasData(new[]
                {
                    new ApplicationRoleClaims()
                    {
                        ClaimType="Permission",
                        ClaimValue="Administrator",
                        RoleId="B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                        Id=2
                    }
                });
            });

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable("AspNetRoles");
                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.HasData(new[]
                {
                    new ApplicationRole()
                    {
                        Id="B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                        Name="Administrator",
                        NormalizedName="ADMINISTRATOR",

                    }
                });
            });

            builder.Entity<ApplicationUserClaims>(entity =>
            {
                entity.ToTable("AspNetUserClaims");
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            builder.Entity<ApplicationUserLogin>(entity =>
            {
                entity.ToTable("AspNetUserLogins");
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            builder.Entity<ApplicationUserRole>(entity =>
            {
                entity.ToTable("AspNetUserRoles");
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId);
                entity.HasData(new[]
                {
                    new ApplicationUserRole()
                    {
                        UserId="c0ea7b0e-e5db-4f17-9c35-87dd2ac20895",
                        RoleId="B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6"
                    }
                });
            });

            builder.Entity<ApplicationUserToken>(entity =>
            {
                entity.ToTable("AspNetUserTokens");
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasMany(x => x.Payments).WithOne(x => x.CreatedByUser).HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);
                entity.ToTable("AspNetUsers");
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
                entity.Property(e => e.FullName).HasComputedColumnSql("FirstName + ' ' + LastName ");
            });
        }

    }
}